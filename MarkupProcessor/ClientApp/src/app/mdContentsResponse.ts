import { MDContents } from "./mdContents";

export interface MDContentsResponse {
  data: MDContents[];
  page: number;
  per_page: number;
  support: any;
  total: number;
  total_pages: number;
}
