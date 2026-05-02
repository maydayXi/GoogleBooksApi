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

### 1.0.0

Initail release - Support searching book information by ISBN

Realeased date: 2026-05-01

## Notes 

This project is for book information search and demonstration purposes.

Search results are provided for reference only. If book information differs from the actual publication, please refer to the publisher, copyright page, or official bibliographic records.