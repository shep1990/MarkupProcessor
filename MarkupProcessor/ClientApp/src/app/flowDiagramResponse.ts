import { FlowDiagram } from "./flowDiagram";

export interface FlowDiagramResponse {
  data: FlowDiagram[];
  page: number;
  per_page: number;
  support: any;
  total: number;
  total_pages: number;
}
