resource "azurerm_cosmosdb_account" "markup-processor-account" {
  name                = var.cosmos_db_name
  location            = var.resource_group_location
  resource_group_name = azurerm_resource_group.markup-processor-rg.name
}


resource "azurerm_cosmosdb_sql_database" "markup-processor-database" {
  name                = "MarkupProcessorData"
  resource_group_name = azurerm_resource_group.markup-processor-rg.name
  account_name        = var.cosmos_db_name
}