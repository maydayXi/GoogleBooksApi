import { UiService } from "./service/search-ui-service.js";
import { SearchController } from "./controller/SearchController.js";

document.addEventListener("DOMContentLoaded", e => {
    
    const $ = id => document.getElementById(id);
    
    const pageSizeWrapper = $("page-size-wrap");
    
    const uiService = new UiService({
        loader: $("loader"), 
        btnSearchBy: $("btn-search-by"), 
        searchByIcon: $("search-by-icon"),
        searchInput: $("book-search-criteria"),
        pageSizeWrapper: pageSizeWrapper,
        pageSizeInput: pageSizeWrapper.querySelector("#page-size"),
        bookSearchResult: $("book-search-result")
    });
    
    const controller = new SearchController(uiService);
    
    uiService.bookSearchResult.addEventListener("click", controller.handlePaginationClick);
    uiService.searchInput.addEventListener("keyup", controller.handleSearchKeyup);
    document.querySelectorAll("button.dropdown-item")
        .forEach(btn => 
            btn.addEventListener("click", controller.handleDropdownClick));
});