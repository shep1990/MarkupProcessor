resource "azurerm_cosmosdb_sql_database" "markupProcessor-database" {
  name                = "MarkupProcessor"
  resource_group_name = data.azurerm_resource_group.markupProcessor-rg.name
  account_name        = data.azurerm_cosmosdb_account.markupProcessor-cosmosdb-account.name
}