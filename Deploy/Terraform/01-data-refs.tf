data "azurerm_resource_group" "markup-processor-rg" {
  name  = "MarkupProcessor-RG"
}

data "azurerm_cosmosdb_account" "markup-processor-cosmosdb-account" {
  name                = "cosmosdb"
  resource_group_name = data.azurerm_resource_group.markup-processor-rg.name
}

