data "azurerm_cosmosdb_account" "markup-processor-cosmosdb-account" {
  name                = "cosmosdb"
  resource_group_name = var.resource_group_name
}

