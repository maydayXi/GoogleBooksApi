/**
 * @fileOverview Abstraction layer over SweetAlert2 for displaying user-facing alerts.
 * @module alert
 * Wraps SweetAlert2 so the rest of the codebase never imports Swal directly (DIP).
 * Swap this file to change the notification library without touching other modules.
 */

/**
 * Shows a validation error alert with a custom message.
 * @param {string} title - The alert title describing the type of validatin error.
 * @param {string} message - A user-friendly description of what went wrong.
 * @returns {void}
 * @example
 * alertValidateionError("Invalid ISBN", "Please enter a valid ISBN-10 or ISBN-13");
 */
export const alertValidationError = (title, message) => {
    Swal.fire({ icon: "error", title, text: message });
};

/**
 * Shows a generic error alert.
 * Use this for unexpected runtime errors or failed API response.
 * @param {string} message - A description of the error to display and log.
 * @returns {void}
 * @example
 * alertError("An error occurred while fetching book data.");
 */
export const alertError = message => {
    Swal.fire({ icon: "error", title: "Error", text: message });
};