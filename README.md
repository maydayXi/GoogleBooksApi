# GoogleBooksApi

A book search web app built with ASP.NET Core MVC and Google Books API

## Features

- Search book information by ISBN
- Support ISNB-10 and ISBN-13
- Support ISBN with or without hyphens
- Fetch book metadata from Google Books API
- Display book title, author, publisher, published date, description, ISBN-10, ISBN-13 and cover image.

## Tech Stack

- ASP.NET Core MVC
- C#
- JavaScript 
- Google Books API

## API Endpoint 

Search book by ISBN

```http
GET /api/googlebooks/bookinfo/{{isbn}}
```

Example:

```http
GET /api/googlebooks/bookinfo/9865026864
```

## Data Source

Book metadata is provided by [Google Books API](https://developers.google.com/books/docs/overview)

The available fields may vary depending on the data returned by Google Books API. Some books may not include complete metadata or cover images.

## Version 

### 1.4.0

refactor(search-js): decouple event handling and split into modular ES6 architecture
- Split monolithic book-search.js into focused ES6 modules (SRP)

- Add `validator.js`: pure ISBN and title validation functions.
- Add `search-strategies.js`: strategy registry mapping search type keys to validate/buildUrl pairs.
- Add `api-service.js`: centralise all HTTP communication.
- Add `search-ui-service.js` (UiService): centralise all DOM read/write operations.
- Add `SearchController.js`: orchestrates search flow by coordinating strategies, API, UI, and alert modules.
- Migrate all event handlers from inline IIFE to SearchController class methods
- Migrate fetch logic from inline callbacks to `api-service.js` (fetchPage, resolveErrorMessage)
- Migrate SweetAlert2 calls to `alert.js` (alertValidationError, alertError)
- Migrate ISBN/title validation to `validator.js`
- Migrate search strategy URL building to `search-strategies.js`

Release date: 2026-05-25

### 1.3.0

feature: Add pagination support for book title search (v1.3.0)

Release data: 2026-05-24

### 1.2.5

fix: improve Google Books response handling and add Serilog logging.

- Add Serilog logging with console and file sinks for error.
- Capture application startup failures and JSON deserialization exceptions.
- Log Google Books request URLs and parsing details to aid Azure troubleshooting.
- Relax Google books DTO property types to handle incomplete or inconsistent API responses.
- Add standardized error responses for JSON parsing failures.

Release date: 2026-05-18

### 1.2.4

fix: allow nullable Google Books description

- Change `GoogleBookVolumeInfoDto.Description` from required string to `string?`
- Allow missing description values from Google Books API
- Improve DTO compatibility with incomplete API responses

Release date: 2026-05-04

### 1.2.3 

fix: releaseDate type in `version.json`

Release date: 2026-05-03

### 1.2.2 

refactor: standardize Google Books API responses

- Update `GoogleBookService` and its interface to return `ApiResponse<T>`.
- Add `GoogleBooksServiceBase` for shared response factory methods.
- Adjust `HomeControll` and `GoogleBooksController` to handle standardized API responses.
- Add `ResponseDataExtension` to simplify ViewModel mapping.
- Extract common file path constants into `WebHelper`.
- Add dotnet-ef tool configuration
- Improve error handling, readability, and maintainability
- fix: `version.json` released date.

Release date: 2026-05-03

### 1.2.1

refactor: load guideline and version data from JSON files

- Replace hardcoded guideline and version data in `HomeController`
- Add `IJsonDataProvider` and `AppDataProvider` for loading static JSON data
- Add data/guideline.json and data/version.json
- Register JSON data provider services in Program.cs
- Improve maintainability of static page content

Release date: 2026-05-03

### 1.2.0 

Support title search and improve book search UI
- Add `BookSearchCriteria` enum for ISBN and title search
- Add `_FetchBooksByTitle` action to return multiple books
- Add `IsbnExtension` for ISBN validation and formatting
- Move shared view models such as `DropdownItemVm` to Components
- Add `_BookCard` partial view for reusable book card layout
- Support switching search criteria from the frontend dropdown
- Fix typo in `isbnValidator.js`
- Improve navbar styling and dropdown selected state

Release date: 2026-05-03

### 1.1.0

Recactor book search flow to use backend Partial View with AJAX loading.
- Added `BookVm` and `ApiResponse` for standardized data structure.
- Extended `WebHelper` with ISBN parsing utility.
- Updated book-search.js: removed frontend card component, improved error handling.
- Split CSS into individual .cshtml.css files for modularity and style isolation.
- Remove googleBooksApi.js to simplify frontend dependencies.

Release date: 2026-05-02

### 1.0.0

Initial release – Support searching book information by ISBN

Released date: 2026-05-01

## Notes 

This project is for book information search and demonstration purposes.

Search results are provided for reference only. If book information differs from the actual publication, please refer to the publisher, copyright page, or official bibliographic records.