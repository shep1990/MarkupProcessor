resource "azurerm_cosmosdb_sql_database" "markup-processor-database" {
  name                = "MarkupProcessor"
  resource_group_name = var.resource_group_name
  account_name        = var.cosmos_db_name
}