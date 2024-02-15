# update conneciton string / sql query in porgram.cs


Update below line with your own db server and User details. Make sure to use ClinetID od Managed Idenitty(userAssigned) as we are on > version 5.1.5 of Microsoft.Data.SqlClient. Link:
https://learn.microsoft.com/en-us/sql/connect/ado-net/sql/azure-active-directory-authentication?view=sql-server-ver16
```
   var connectionString = "Server=demo.database.windows.net; Authentication=Active Directory Managed Identity; Encrypt=True; User Id=ObjectIdOfManagedIdentity; Database=testdb";

```


# build/push image
```
az acr login --name ariantestacr001

cd sql-wi
docker build -t sql-witest:1.0.0 -f dockerfile .
docker tag sql-witest:1.0.0 ariantestacr001.azurecr.io/sql-witest:1.0.0
docker push ariantestacr001.azurecr.io/sql-witest:1.0.0
```



# SQL Config:

```
CREATE USER [<MI-name>] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [<MI-name>];
ALTER ROLE db_datawriter ADD MEMBER [<MI-name>];
ALTER ROLE db_ddladmin ADD MEMBER [<MI-name>];
GO
```


# K8S deploy

```
az aks get-credentials --resource-group aks --name anevjes-aks --overwrite-existing
cd ..
cd deploy
kubectl create namespace sql-wi-test
kubectl apply -f serviceaccount.yaml
kubectl apply -f deployment.yaml
```