/**
 * Google Books Data Search
 * This JavaScript module provides functions to search for books using the Google Books API.
 */

(function () {
    // test isbn = 9865026864
    const $ = id => document.getElementById(id);

    let loader = $("loader"),
        btnSearchBy = $("btn-search-by"),
        searchByIcon = $("search-by-icon"),
        searchInput = $("book-search-criteria"),
        dropdownBtns = document.querySelectorAll("button.dropdown-item");

    let bookSearchResult = $("book-search-result"),
        bookTitle = $("book-title"),
        bookSubTitle = $("book-subtitle"),
        bookImage = $("book-image"),
        bookDescription = $("book-description"),
        publishedDate = $("published-date"),
        isbn10 = $("isbn-10"),
        isbn13 = $("isbn-13");

    const INVISIBLE_CLASS = "d-none";

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
                const isbnValidationResult = isbnValidator.validate(searchTerm);

                if (!isbnValidationResult.isValid) {
                    Swal.fire({
                        icon: "error",
                        title: "Invalid ISBN",
                        text: "Please enter a valid ISBN-10 or ISBN-13 number."
                    });

                    return;
                }

                const response = await googleApiClient.fetchByIsbnAsync(searchTerm);

                bookImage.style = `background-image: url(${response.imageLink})`;
                bookTitle.textContent = response.title || "";
                bookSubTitle.querySelector("span").textContent = `${response.author} - ${response.publisher}`

                bookDescription.textContent =
                    `${response.description?.substring(0, 130)}${response.description ? "..." : ""}`;

                const { bookIdentifier } = response;
                isbn10.textContent = bookIdentifier?.ISBN_10 || "";
                isbn13.textContent = bookIdentifier?.ISBN_13 || "";

                publishedDate.querySelector("span").textContent = response.publishedDate || "";

                bookSearchResult.classList.remove(INVISIBLE_CLASS);
                searchInput.value = "";
            }
            catch (error) {
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: error.message || "An error occurred while fetching book data."
                });
                console.error("Error fetching book data:", error);
                bookSearchResult.classList.add(INVISIBLE_CLASS);
            }
            finally {
                loader.classList.add(INVISIBLE_CLASS);
            }
        }
    };

    searchInput.addEventListener('keyup', handleSearchInputKeyup);
    dropdownBtns.forEach(btn => btn.addEventListener("click", handleDropdownClick));
})();