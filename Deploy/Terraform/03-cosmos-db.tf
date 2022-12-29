resource "azurerm_cosmosdb_sql_database" "markup-processor-database" {
  name                = "MarkupProcessor"
  resource_group_name = data.azurerm_resource_group.markup-processor-rg.name
  account_name        = data.azurerm_cosmosdb_account.markup-processor-cosmosdb-account.name
}