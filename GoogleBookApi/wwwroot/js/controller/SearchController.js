import { getStrategy } from "../book-search/search-strategies.js";
import { fetchBook, fetchPage, resolveErrorMessage} from "../service/api-service.js";
import { alertValidationError, alertError } from "../util/alert.js";

/**
 * Fallback error message used when no specific error message is available.
 * @type {string}
 */
const DEFAULT_ERROR_MESSAGE = "An unexpected error occurred.";

/**
 * Represents the search criteria used to fetch paginated book results.
 * Stored after a successful search so pagination can reuse the same query.
 * @typedef {Object} Criteria
 * @property {string} apiUrl - The API endpoint URL built from the active search strategy.
 * @property {number} pageSize - The number of the results to return per page.
 */

/**
 * @fileOverview Search controller that orchestrates the book search flow.
 * @module search-controller
 * Orchestrates user interactions by coordinating strategies, API service, UI service, and the alert. 
 * This is the only layer that knows about "flow" (DIP, SRP).
 */
export class SearchController {
    /**
     * Create a new SearchController instance.
     * @param {import("../service/book-ui-service.js").UiService} uiService
     */
    constructor(uiService) {
        this._ui = uiService;

        /**
         * The key identifying the currently active search strategy (e.g. `"isbn"` or `"title"`).  
         * @type {string}
         */
        this._currentStrategyKey = "isbn";

        /** 
         * The search criteria from the most recent successful search.
         * @type {Criteria | null} 
         */
        this._lastCriteria = null;
    }

    // #region Event handler
    /**
     * Updates the active search strategy when a dropdown item is clicked.
     * @param {Event} e The click event triggered by the dropdown item button.
     */
    handleDropdownClick = e => {
        const btn = e.currentTarget;
        const key = (btn.dataset.searchBy ?? "isbn").toLowerCase();

        this._currentStrategyKey = key;
        this._ui.updateSearchByButton(btn);
        this._ui.setPageSizeVisible(key !== "isbn");
    };

    /**
     * Triggers a search when the Enter key is pressed in the search input.
     * @param {KeyboardEvent} e The keyup event triggered by the search input field.
     */
    handleSearchKeyup = async e => {
        if (e.key !== "Enter") return;

        e.preventDefault();
        this._ui.showLoader();

        try {
            const searchTerm = this._ui.getSearchTerm();
            const strategy = getStrategy(this._currentStrategyKey);
            const { isValid, errorMessage } = strategy.validate(searchTerm);

            if (!isValid) {
                alertValidationError(`Invalid ${this._currentStrategyKey}`, errorMessage);
                return;
            }

            this._lastCriteria = {
                apiUrl: strategy.buildUrl(searchTerm),
                pageSize: this._ui.getPageSize(),
            };
            
            this._currentStrategyKey !== "isbn"
                ? await this._loadPage(1)
                : await this._loadBook(this._lastCriteria.apiUrl);
            
            this._ui.clearSearchInput();
        } catch (err) {
            alertError(err.message ?? DEFAULT_ERROR_MESSAGE);
            console.error("[SearchController] Search error:", err);
        } finally {
            this._ui.hideLoader();
        }
    };

    /**
     * Handles pagination button clicks via event delegation.
     * @param {Event} e
     */
    handlePaginationClick = async e => {
        const btn = e.target.closest("#pagination .btn:not(.disabled):not(.active)");
        if (!btn) return;

        const page = parseInt(btn.textContent.trim(), 10);
        if (isNaN(page)) return;

        this._ui.showLoader();

        try {
            await this._loadPage(page);
        } catch (err) {
            alertError(err.message ?? DEFAULT_ERROR_MESSAGE);
            console.error("[SearchController] Pagination error:", err);
        } finally {
            this._ui.hideLoader();
        }
    };
    // #endregion

    // #region Private Helpers
    async _loadBook(url) {
        if(!url) return;
        
        const response = await fetchBook(url);
        const errorMessage = await resolveErrorMessage(response);
        if(errorMessage) {
            alertError(errorMessage);
            return;
        }
        
        this._ui.renderSearchResult(await response.text());
    }
    
    /**
     * Fetches a result page and renders it, or shows an error.
     * @param {number} page The one-based page number to fetch.
     */
    async _loadPage(page) {
        if (!this._lastCriteria) return;

        const response = await fetchPage(this._lastCriteria, page);
        const errorMessage = await resolveErrorMessage(response);

        if (errorMessage) {
            alertError(errorMessage);
            return;
        }

        this._ui.renderSearchResult(await response.text());
    }
    // #endregion
}