/**
 * ISBN Validator
 */

/**
 * JSDoc Type definition for the validation result of an ISBN.
 * 
 * @typedef {Object} IsbnValidationResult
 * @property {boolean} isValid - Indicates whether the ISBN is valid.
 * @property {string|null} type - The type of the ISBN ("ISBN-10" or "ISBN-13"), or null if the ISBN is invalid.
 * @property {string} normalizedIsbn - The normalized form of the ISBN (without hyphens or spaces).
 */
const isbnValidator = (() => {

    /**
     * Normalizes an ISBN by removing any hyphens or spaces.     
     * @param {string} isbn - The ISBN to normalize.
    * @returns {string} The normalized ISBN, or an empty string if the input is null or undefined.
     */
    const normalizeIsbn = isbn => !isbn 
        ? ""
        : isbn.trim().replace(/[-\s]/g, '');

    /**
     * Checks if the given ISBN-10 is valid.
     * @param {string} isbn - The ISBN-10 to validate.    
     */
    const isValidIsbn10 = isbn => {
        if (!/^[0-9]{9}[0-9x]$/.test(isbn)) return false;

        let weights = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        let result = weights.reduce((sum, weight, index) => sum + Number(isbn[index]) * weight, 0);

        const checkDigit = isbn[9].toLowerCase() === 'x'
            ? 10
            : Number(isbn[9]);

        result += checkDigit;

        return result % 11 === 0;
    };

    /**
     * Checks if the given ISBN-13 is valid.
     * @param {string} isbn - The ISBN-13 to validate.
     */
    const isValidIsbn13 = isbn => {
        if (!/^[0-9]{13}$/.test(isbn)) return false;

        let weights = [1, 3];
        let result = isbn.split('').slice(0, 12)
            .reduce((sum, digit, index) => sum + Number(digit) * weights[index % 2], 0);

        const checkDigit = (10 - (result % 10)) % 10;

        return checkDigit === Number(isbn[12]);
    };

    /**
     * Checks if the given ISBN is valid. It supports both ISBN-10 and ISBN-13 formats.
     * @param {string} isbn - The ISBN to validate.
     */
    const isValid = isbn => {
        const normalizedIsbn = normalizeIsbn(isbn);

        if (isbn.length === 10) return isValidIsbn10(normalizedIsbn);
        if (isbn.length === 13) return isValidIsbn13(normalizedIsbn);

        return false;
    };

    /**
     * Gets the type of the given ISBN (either "ISBN-10" or "ISBN-13"). 
     * It returns null if the input is not a valid ISBN.
     * @param {string} input - The ISBN to check the type of.
     * @returns {string|null} "ISBN-10" or "ISBN-13" or null if the input is not a valid ISBN.
     */
    const getType = input => {
        const isbn = normalizeIsbn(input);

        if (isValidIsbn10(isbn)) return "ISBN-10";
        if (isValidIsbn13(isbn)) return "ISBN-13";

        return null;
    };

    /**
     * Validates the given ISBN and returns an object containing 
     * the validation result, 
     * the type of ISBN, 
     * and the normalized ISBN.
     * @param {string} isbn - The ISBN to validate.
     * @returns {IsbnValidationResult} An object containing the validation result, type, and normalized ISBN.
     */
    const validate = isbn => {
        const normalizedIsbn = normalizeIsbn(isbn),
            type = getType(normalizedIsbn);

        return {
            isValid: type !== null,
            type,
            normalizedIsbn
        };
    };

    return {
        validate
    };
})();