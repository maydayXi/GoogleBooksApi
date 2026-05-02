/**
 * Google Books API Client
 * This JavaScript module provides functions to fetch books data with the Google Books API.
 */

/**
 * JSDoc Type definetion (Description Google Books API response data)
 *
 * @typedef {Object} GoogleBookResponse
 * @property {string} [title] - The title of the book.
 * @property {string} [author] - The author of the book.
 * @property {string} [publisher] - The publisher of the book.
 * @property {string} [publishedDate] - The published date of the book.
 * @property {string} [description] - A description of the book.
 * @property {string} [imageLink] - The image link of the book cover.
 * @property {BookIdentifier} [bookIdentifier] - The identifiers of the book.
 * 
 * @typedef {Object} BookIdentifier
 * @property {string} [ISBN_10] - The 10-digit ISBN identifier.
 * @property {string} [ISBN_13] - The 13-digit ISBN identifier.
 */

/**
 * GoogleBookApi client module
 */
const googleApiClient = (() => {
    const baseUrl = "/api/googlebooks"; 

    /**
     * Fetches book data by ISBN from the Google Books API.
     * @param {string} isbn - The ISBN of the book to fetch.
     * @returns {Promise<GoogleBookResponse>} - A promise that resolves to the book data (see {@link GoogleBookVolume}).
     *
     * @example
     * // Usage:
     * // googleApiClient.fetchByIsbnAsync('9780143127550').then(volume => console.log(volume));
     */
    async function fetchByIsbnAsync(isbn) {
        if (!isbn || isbn.trim() === "") {
            throw new Error("ISBN must be provided.");
        }

        const url = `${baseUrl}/bookinfo/${encodeURIComponent(isbn.trim())}`;

        const response = await fetch(url, {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        });

        if (!response.ok) {
            let errorMessage = `Error fetching book data: ${response.status} \n${response.statusText}`;

            try {
                const errorBody = await response.json();

                if (errorBody && errorBody.error && errorBody.error.message) 
                    errorMessage += `\nDetails: ${errorBody.error.message}`;
            }
            catch {
            }

            throw new Error(errorMessage);
        }

        return /** @type {Promise<GoogleBookVolume>} */ (await response.json());
    }

    return {
        fetchByIsbnAsync
    }
})();