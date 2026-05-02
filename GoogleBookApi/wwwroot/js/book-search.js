/**
 * Google Books Data Search
 * This JavaScript module provides functions to search for books using the Google Books API.
 */

/**
 * JSDoc Type definition for search action 
 * 
 * @typedef {Object} BookSearchOption -  The result of the validation and the API URL for the book search.
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
        dropdownBtns = document.querySelectorAll("button.dropdown-item");

    let bookSearchResult = $("book-search-result");

    const INVISIBLE_CLASS = "d-none";

    /**
     * Book search actions
     * @type {Object<string, BookSearchAction>}
     */
    const bookSearchActions = {
        /**
         * Validate the input as an ISBN number and return the appropriate API URL and SweetAlert configuration.
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
                apiUrl: `/${encodeURIComponent('_FetchBooksByTitle')}/${encodeURIComponent(input)}`
            }
        }
    }

    /**
     * Current search criteria for book search. Default is "isbn".
     */
    let currentSearchBy = "isbn";

    /**
     * Handle dropdown item click event to update the search criteria.
     * @param {Event} e - The click event triggered by the dropdown item.
     */
    const handleDropdownClick = e => {
        /**
         * The dropdown button element thad triggered the clikck event.
         * @type {HTMLButtonElement}
         */
        const currentBtn = e.currentTarget;
        const btnIconCssClass = currentBtn?.querySelector("i")?.getAttribute("class") || INVISIBLE_CLASS;

        // get button display text.
        const text = (currentBtn && currentBtn.textContent) ? currentBtn.textContent.trim() : "";
        searchByIcon.setAttribute("class", btnIconCssClass);
        btnSearchBy.querySelector("span").textContent = text;

        currentSearchBy = (currentBtn.dataset.searchBy || "isbn").toLocaleLowerCase();
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

            let defaultErrorMessage = "An error occurred while fetching book data.";
            try {
                const bookSearchAction = bookSearchActions[currentSearchBy];
                const { passValidation, sweetAlertConfig, apiUrl } = bookSearchAction(searchTerm);

                if (!passValidation) {
                    Swal.fire(sweetAlertConfig);
                    return;
                }

                const response = await fetch(apiUrl, {
                    method: "GET",
                    headers: {
                        'Accept': 'text/html'
                    }
                });

                if (!response.ok) {
                    const message = await response.text();

                    if (response.status === 400 || response.status === 404)
                        defaultErrorMessage = response.status === 400 ? "Bad Request" : "Book Not Found";

                    Swal.fire({
                        icon: "error",
                        title: "Error",
                        text: message || defaultErrorMessage
                    });
                    return;
                }

                bookSearchResult.innerHTML = await response.text();
                searchInput.value = "";
            }
            catch (error) {
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: error.message || defaultErrorMessage
                });
                console.error("Error fetching book data:", error);
            }
            finally {
                loader.classList.add(INVISIBLE_CLASS);
            }
        }
    };

    searchInput.addEventListener('keyup', handleSearchInputKeyup);
    dropdownBtns.forEach(btn => btn.addEventListener("click", handleDropdownClick));
})();