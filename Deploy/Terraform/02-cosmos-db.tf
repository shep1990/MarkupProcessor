resource "azurerm_cosmosdb_account" "markup-processor-account" {
  name                = var.cosmos_db_name
  location            = var.resource_group_location
  resource_group_name = data.azurerm_resource_group.markup-processor-rg.name
  offer_type          = "Standard"
    consistency_policy {
    consistency_level       = "BoundedStaleness"
    max_interval_in_seconds = 300
    max_staleness_prefix    = 100000
  }
    geo_location {
    location          = var.resource_group_location
    failover_priority = 0
  }
}


resource "azurerm_cosmosdb_sql_database" "markup-processor-service-db" {
  name                = "markup-processor-database"
  resource_group_name = data.azurerm_resource_group.markup-processor-rg.name
  account_name        = azurerm_cosmosdb_account.markup-processor-account.name
}

resource "azurerm_cosmosdb_sql_container" "markup-processor" {
  name                  = "markup-processor-container"
  resource_group_name   = data.azurerm_resource_group.markup-processor-rg.name
  account_name          = azurerm_cosmosdb_account.markup-processor-account.name
  database_name         = azurerm_cosmosdb_sql_database.markup-processor-service-db.name
  partition_key_path    = "/flowChartId"
  partition_key_version = 1
  default_ttl           = 1209600

  indexing_policy {

    included_path {
      path = "/*"
    }

    included_path {
      path = "/included/?"
    }

    excluded_path {
      path = "/\"_etag\"/?"
    }
  }
}