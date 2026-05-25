/**
 * ISBN validator utility imported from the isbnValidator utility module.
 * @external validateIsbn
 * @see module:util/isbnValidator
 */
import { validateIsbn as validate } from "../util/isbnValidator.js";

/**
 * @fileOverview Pure validation functions for the book search inputs.
 * @module validators
 * Pure validation functions — no DOM, no side effects.
 */

/**
 * @typedef {Object} ValidationResult
 * @property {boolean} isValid - Indicates whether the input passed validation.
 * @property {string|null} errorMessage - Error message if validation failed; null if valid.
 */

/**
 * Validates an ISBN-10 or ISBN-13 string.
 * @param {string} isbn - ISBN-10 or ISBN-13 with or without hyphens.
 * @returns {ValidationResult} - The result of isbn validation.
 */
export const validateIsbn = isbn => {
    const isValidIsbn = validate(isbn).isValid;
    return {
        isValid: isValidIsbn,
        errorMessage: isValidIsbn ? null : "Please enter a valid ISBN-10 or ISBN-13 number.",
    };
};

/**
 * Validates a book title string.
 * @param {string} input - The book title entered by the user.
 * @returns {ValidationResult} - The result of the title validation.
 */
export const validateTitle = input => {
    const isValid = typeof input === "string" && input.trim().length > 0;
    return {
        isValid,
        errorMessage: isValid ? null : "Please enter a book title.",
    };
};