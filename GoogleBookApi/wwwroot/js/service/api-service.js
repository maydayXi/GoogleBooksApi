/**
 * @module api-service
 * Handles all HTTP communication. No UI, no validation logic here (SRP).
 */

const DEFAULT_PAGE_SIZE = 10;

const DEFAULT_PAGE = 1;

const statusMessages = {
    400: "Bad Request — please check your search input.",
    404: "No results found for your search.",
};

/**
 * @typedef {Object} SearchCriteria
 * @property {string} apiUrl  - Base API URL (without pagination params)
 * @property {number} pageSize
 */

/**
 * Fetches an HTML partial view for given URL.
 * @param {string} url 
 * @returns {Promise<Response>}
 */
export const fetchBook = async (url) => 
    fetch(url, {
        method: "GET",
        headers: { Accept: "text/html"}
    });

/**
 * Fetches an HTML partial view for the given page.
 * @param {SearchCriteria} criteria
 * @param {number} [page=1]
 * @returns {Promise<Response>}
 */
export const fetchPage = async (criteria, page = DEFAULT_PAGE) => {
    const { apiUrl, pageSize = DEFAULT_PAGE_SIZE } = criteria;
    const url = `${apiUrl}&pagesize=${pageSize}&page=${page}`;

    return fetch(url, {
        method: "GET",
        headers: { Accept: "text/html" },
    });
};

/**
 * Resolves a user-friendly error message from a failed response.
 * Returns an empty string when the response is OK.
 * @param {Response} response
 * @returns {Promise<string>}
 */
export const resolveErrorMessage = async response => {
    if (response.ok) return "";

    return statusMessages[response.status] 
        ?? "An error occurred while fetching book data.";
};