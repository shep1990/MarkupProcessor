resource "azurerm_cosmosdb_sql_database" "markup-processor-database" {
  name                = "MarkupProcessor"
  resource_group_name = azurerm_resource_group.markup-processor-rg.name
  account_name        = var.cosmos_db_name
}