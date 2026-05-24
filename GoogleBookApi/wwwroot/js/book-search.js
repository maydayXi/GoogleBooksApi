/**
 * Google Books Data Search
 * This JavaScript module provides functions to search for books using the Google Books API.
 */

/**
 * JSDoc Type definition for search action 
 * 
 * @typedef {Object} BookSearchOption - The result of the validation and the API URL for the book search.
 * @property {bool} passValidation - Indicates whether the input passed validation.
 * @property {Object|null} sweetAlertConfig - The configuration object for SweetAlert if validation fails, or null if validation passes.
 * @property {string} apiUrl - The API URL for fetching book data based on the search criteria.
 */

/**
 * JSDoc Type definition for book search action 
 * 
 * @callback BookSearchAction
 * @param {string} input - The user input to validate and use for the book search.
 * @returns {BookSearchOption}
 */

(function () {
    // test isbn = 9865026864
    const $ = id => document.getElementById(id);

    let loader = $("loader"),
        btnSearchBy = $("btn-search-by"),
        searchByIcon = $("search-by-icon"),
        searchInput = $("book-search-criteria"),
        dropdownBtns = document.querySelectorAll("button.dropdown-item"),
        pageSizeWrapper = $("page-size-wrap"),
        pageSize = pageSizeWrapper.querySelector("#page-size");
    
    let bookSearchResult = $("book-search-result");

    const INVISIBLE_CLASS = "d-none";

    /**
     * Stores the most recent search criteria submitted by the user.
     * @type {{api: string, size: number}}
     * @property {string} api - The full API request URL used in the last search
     * @property {number} size - The number of the results requested in the last search.
     */
    const lastSearchCriteria = {
        api: "",
        size: 0
    };

    /**
     * Book search actions
     * @type {Object<string, BookSearchAction>}
     */
    const bookSearchActions = {
        /**
         * Validate the input as an ISBN and return the appropriate API URL and SweetAlert configuration.
         */
        isbn: input => {
            const isValidInput = isbnValidator.validate(input).isValid;

            return {
                passValidation: isValidInput,
                sweetAlertConfig: isValidInput
                    ? null
                    : {
                        icon: "error",
                        title: "Invalid ISBN",
                        text: "Please enter a valid ISBN-10 or ISBN-13 number."
                    },
                apiUrl: `/${encodeURIComponent('_FetchBookByIsbn')}/${encodeURIComponent(input)}`
            }
        },
        /**
         * Validate the input as a book title and return the appropriate API URL and SweetAlert configuration.
         */
        title: input => {
            const isValidInput = !!input || input.trim().length > 0;

            return {
                passValidation: isValidInput,
                sweetAlertConfig: isValidInput
                    ? null
                    : {
                        icon: "error",
                        title: "Invalid Title",
                        text: "Please enter book title."
                    },
                apiUrl: `/_FetchBooksByTitle?title=${input}`
            }
        }
    }

    /**
     * Current search criteria for book search. Default is "isbn".
     */
    let currentSearchBy = "isbn";

    /**
     * Fetches the paginated book search result as an HTML partial view.
     * Uses the last search criteria stored in {@link lastSearchCriteria}.
     * @param {number} page - The page number to fetch. Defaults to 1 if not provided.
     * @returns {Promise<Response>} The fetch response containing the rendered HTML.
     */
    const fetchBookByPage = async page => {
        let currentPage = page || 1;
        
        const { size, api } = lastSearchCriteria;

        return await fetch(`${api}&pagesize=${size}&page=${currentPage}`, {
            method: "GET",
            headers: {'Accept': 'text/html'}
        });
    };

    /**
     * Extracts a user-friendly error message from a failed fetch response.
     * @param {Response} response - The fetch response to evaluate.
     * @returns {Promise<string>} A descriptive error message, or an empty string if no error.
     */
    const getErrorMessageFromResponse = async response => {
        if(response.ok) return "";
        
        const message = await response.text();
        let errorMessage = "An error occurred while fetching book data.";
        
        if(response.status === 400 || response.status === 404)
            errorMessage = response.status === 400 ? "Bad Request" : "Not found";
        
        return errorMessage;
    };

    /**
     * Show error message via Sweetalert
     * @param {string} errorMessage - error message 
     */
    const showError = errorMessage => {
        Swal.fire({
            icon: "error",
            title: "Error",
            text: errorMessage
        });
    };

    /**
     * Handle the dropdown item click event to update the search criteria.
     * @param {Event} e - The click event triggered by the dropdown item.
     */
    const handleDropdownClick = e => {
        /**
         * The dropdown button element that triggered the click event.
         * @type {HTMLButtonElement}
         */
        const currentBtn = e.currentTarget;
        const btnIconCssClass = currentBtn?.querySelector("i")?.getAttribute("class") || INVISIBLE_CLASS;

        // get button display text.
        const text = (currentBtn && currentBtn.textContent) ? currentBtn.textContent.trim() : "";
        searchByIcon.setAttribute("class", btnIconCssClass);
        btnSearchBy.querySelector("span").textContent = text;

        currentSearchBy = (currentBtn.dataset.searchBy || "isbn").toLocaleLowerCase();
        
        pageSizeWrapper.classList.toggle(INVISIBLE_CLASS, currentSearchBy === "isbn");
    };

    /**
     * Handle keyup event on the search input to trigger a search when the Enter key is pressed.
     * @param {Event} e - The keyup event triggered by the search input.
     */
    const handleSearchInputKeyup = async e => {
        const key = e.key || e.keyCode;
        if (key === 'Enter' || key === 13) {
            e.preventDefault();
            loader.classList.remove(INVISIBLE_CLASS);
            const searchTerm = searchInput.value.trim();

            try {
                const bookSearchAction = bookSearchActions[currentSearchBy];
                const { passValidation, sweetAlertConfig, apiUrl } = bookSearchAction(searchTerm);

                if (!passValidation) {
                    Swal.fire(sweetAlertConfig);
                    return;
                }
                
                let size = parseInt(pageSize.value);
                lastSearchCriteria.api = apiUrl;
                lastSearchCriteria.size = isNaN(size) ? 10 : size;

                const response = await fetchBookByPage(1);

                const errorMessage = await getErrorMessageFromResponse(response);
                if(errorMessage) {
                    showError(errorMessage);
                    return;
                }

                bookSearchResult.innerHTML = await response.text();
                searchInput.value = "";
            }
            catch (error) {
                showError(error.message || defaultErrorMessage);
                console.error("Error fetching book data:", error);
            }
            finally {
                loader.classList.add(INVISIBLE_CLASS);
            }
        }
    };

    /**
     * Handles click events delegated from the pagination container.
     * @param {Event} e - The click event triggered within the book search result container.
     */
    const handlePageBtnClicked = async e => {
        loader.classList.remove(INVISIBLE_CLASS);
        try {
            const btn = e.target.closest("#pagination .btn:not(.disabled):not(.active)");
            if(!btn) return;
            
            const currentPage = parseInt(btn.textContent.trim());
            if(isNaN(currentPage)) return;

            const response = await fetchBookByPage(currentPage);
            const errorMessage = await getErrorMessageFromResponse(response);
            
            if(errorMessage) {
                showError(errorMessage);
                return;
            }

            bookSearchResult.innerHTML = await response.text();
            searchInput.value = "";
        }
        catch (error) {
            showError(error.message || defaultErrorMessage)
            console.error("Error fetching book data:", error);
        }
        finally {
            loader.classList.add(INVISIBLE_CLASS);
        }
    };
    
    bookSearchResult.addEventListener('click', handlePageBtnClicked);
    searchInput.addEventListener('keyup', handleSearchInputKeyup);
    dropdownBtns.forEach(btn => btn.addEventListener("click", handleDropdownClick));
})();