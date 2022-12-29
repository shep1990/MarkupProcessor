data "azurerm_resource_group" "markupProcessor-rg" {
  name  = "MarkupProcessor-RG"
}

data "azurerm_cosmosdb_account" "markupProcessor-cosmosdb-account" {
  name                = "cosmosdb"
  resource_group_name = data.azurerm_resource_group.markupProcessor-rg.name
}