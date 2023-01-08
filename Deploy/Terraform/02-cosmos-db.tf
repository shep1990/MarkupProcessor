resource "azurerm_cosmosdb_account" "markup-processor-account" {
  name                = var.cosmos_db_name
  location            = var.resource_group_location
  resource_group_name = azurerm_resource_group.markup-processor-rg.name
  offer_type          = "Standard"
    consistency_policy {
    consistency_level       = "BoundedStaleness"
    max_interval_in_seconds = 300
    max_staleness_prefix    = 100000
  }
    geo_location {
    location          = "westeu"
    failover_priority = 0
  }
}


resource "azurerm_cosmosdb_sql_database" "markup-processor-database" {
  name                = "MarkupProcessorData"
  resource_group_name = azurerm_resource_group.markup-processor-rg.name
  account_name        = var.cosmos_db_name
}