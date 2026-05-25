/**
 * @fileOverview Search strategy registry for book search types.
 * @module search-strategies
 * Each strategy encapsulates validation and URL building for one search type.
 * Add new search types here without touching any other module (OCP).
 *
 * Strategy interface:
 *   validate (input: string): ValidationResult
 *   buildUrl (input: string): string
 */

import { validateIsbn, validateTitle } from "./validator.js";

/**
 * @typedef {Object} StrategyValue
 * @property {Function} validate - Validate the user input for the specific search type.
 * @property {Function} buildUrl - Builds the API request URL based on the validated inut.
 */

/**
 * A registry mapping search type keys to their corresponding search strategies.
 * 
 * Supported keys:
 * - `"isbn"` - validates ISBN-10 or ISBN-13 and builds the fetch-by-ISBN endpoint URL.
 * - `"title"` - validates a non-empty title string and builds the fetch-by-Title endpoint URL.
 * 
 * @type {Map<string, StrategyValue>} 
 */
export const searchStrategyRegistry = new Map([
    [
        "isbn",
        {
            validate: validateIsbn,
            buildUrl: input =>
                `/${encodeURIComponent("_FetchBookByIsbn")}/${encodeURIComponent(input)}`,
        },
    ],
    [
        "title",
        {
            validate: validateTitle,
            buildUrl: input =>
                `/_FetchBooksByTitle?title=${encodeURIComponent(input)}`,
        },
    ],
]);

/**
 * Retrieves a search strategy by key.
 * @param {string} key - The search type key (e.g. `"isbn"` or `"title"`)
 * @returns {StrategyValue}  The strategy object ossociated with the given key.
 * @throws {Error} if the key is not registered
 * @example
 * const stratey = getStratey("isbn");
 * const { isValid, errorMessage } = strategy.validate(isbn);
 * const url = strategy.buildUrl(isbn);
 */
export const getStrategy = key => {
    const strategy = searchStrategyRegistry.get(key);
    if (!strategy) throw new Error(`Unknown search strategy: "${key}"`);
    return strategy;
};