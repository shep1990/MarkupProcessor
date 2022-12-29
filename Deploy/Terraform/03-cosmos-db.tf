resource "azurerm_cosmosdb_sql_database" "markup-processor-database" {
  name                = "MarkupProcessor"
  resource_group_name = var.resource_group_name
  account_name        = data.azurerm_cosmosdb_account.markup-processor-cosmosdb-account.name
}