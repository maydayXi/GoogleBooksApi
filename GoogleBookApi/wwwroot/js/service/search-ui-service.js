/**
 * @fileOverview UI service for managing all DOM read/write operations related to book search.
 * @module ui-service
 * All DOM read/write operations in one place (SRP).
 * Receives element references via constructor so it can be tested with mocks (DIP).
 */

/**
 * @typedef {Object} UIElements
 * @property {HTMLElement} loader - The full-screen loading overlay element.
 * @property {HTMLElement} btnSearchBy - The dropdown toggle button for selecting search type.
 * @property {HTMLElement} searchByIcon - The icon element inside the search-by dropdown button.
 * @property {HTMLInputElement} searchInput - The text input element for entering search terms.
 * @property {HTMLElement} pageSizeWrapper - The container element for the page-size selector.
 * @property {HTMLSelectElement} pageSizeInput - The select element for choosing the number of results per page.
 * @property {HTMLElement} bookSearchResult - The container element where search results are rendered.
 */

/**
 * CSS class used to hide elements.
 * @constant {string}
 */
const INVISIBLE_CLASS = "d-none";

/**
 * Service class responsible for all DOM read/write operations related to the book search UI.
 * Centralizes DOM access in one place (SRP) and accepts element references via constructorto support mocking in tests (DIP).
 */
export class UiService {
    /**
     * Creates a new UiService instance with the provided DOM element references.
     * @param {UIElements} elements - The set of DOM elements required by the service.
     */
    constructor(elements) {
        const {
            loader,
            searchInput,
            searchByIcon,
            btnSearchBy,
            pageSizeWrapper,
            pageSizeInput,
            bookSearchResult
        } = elements;
        this.loader = loader;
        this.searchInput = searchInput;
        this.searchByIcon = searchByIcon;
        this.btnSearchBy = btnSearchBy;
        this.pageSizeWrapper = pageSizeWrapper;
        this.pageSizeInput = pageSizeInput;
        this.bookSearchResult = bookSearchResult;
    }

    /**
     * Shows the full-screen loading overlay.
     * @returns {void}
     */
    showLoader() {
        this.loader.classList.remove(INVISIBLE_CLASS);
    }

    /**
     * Hides the full-screen loading overlay.
     * @returns {void}
     */
    hideLoader() {
        this.loader.classList.add(INVISIBLE_CLASS);
    }

    /**
     * Reads and trims the current value from the search input field.
     * @returns {string} The trimmed search term entered by the user.
     */
    getSearchTerm() {
        return this.searchInput.value.trim();
    }

    /**
     * Clears the search input field.
     * @returns {void}
     */
    clearSearchInput() {
        this.searchInput.value = "";
    }

    /**
     * Reads the selected page size from the page-size selector.
     * Falls back to 10 if the value cannot be parsed as an integer.
     * @returns {number} The selected page size, or 10 as the default.
     */
    getPageSize() {
        const parsedNumber = parseInt(this.pageSizeInput.value, 10);
        return isNaN(parsedNumber) ? 10 : parsedNumber;
    }

    /**
     * Renders an HTML string into the book search result container.
     * @param {string} html - The HTML string to inject as the search result content.
     * @returns {void}
     */
    renderSearchResult(html) {
        this.bookSearchResult.innerHTML = html;
    }

    /**
     * Updates the dropdown button label and icon to reflect the selected search type.
     * @param {HTMLButtonElement} dropdownBtn - The dropdown item button that was clicked.
     * @returns {void}
     */
    updateSearchByButton(dropdownBtn) {
        const iconClass =
            dropdownBtn.querySelector("i")?.getAttribute("class")
            ?? INVISIBLE_CLASS;
        
        const label = dropdownBtn.textContent ?? "";

        this.searchByIcon.setAttribute("class", iconClass);
        this.btnSearchBy.querySelector("span").textContent = label.trim();
    }

    /**
     * Shows or hides the page-size control depending on the active search type.
     * The page-size selector is only relevant for title search, not ISBN.
     * @param {boolean} visible - Pass `true` to show the control, `false` to hide it.
     * @returns {void}
     */
    setPageSizeVisible(visible) {
        this.pageSizeWrapper.classList.toggle(INVISIBLE_CLASS, !visible);
    }
}