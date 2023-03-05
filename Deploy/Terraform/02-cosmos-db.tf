resource "azurerm_cosmosdb_sql_database" "markup-processor-service-db" {
  name                = "markup-processor-database"
  resource_group_name = data.azurerm_resource_group.markup-processor-rg.name
  account_name        = data.azurerm_cosmosdb_account.markup-processor-cosmosdb-account.name
}

resource "azurerm_cosmosdb_sql_container" "markup-processor" {
  name                  = "markup-processor-container"
  resource_group_name   = data.azurerm_resource_group.markup-processor-rg.name
  account_name          = data.azurerm_cosmosdb_account.markup-processor-cosmosdb-account.name
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