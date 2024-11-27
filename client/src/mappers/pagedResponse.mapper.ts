import { PagedResponse } from "@/types/fetch";

export default class PagedResponseMapper {
    static fromPageResponse<T>(pagedResponse: PagedResponse<T>): T[] {
        return pagedResponse.results
    }
}