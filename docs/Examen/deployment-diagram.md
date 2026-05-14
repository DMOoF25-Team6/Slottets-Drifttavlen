
# Deploymentsdiagram


## Kørsel på lokal maskine som monolitisk applikation

**Scenarie:**
Denne opsætning anvendes til lokal udvikling og test, hvor hele applikationen kører som en monolit på én maskine. Alle komponenter er tilgængelige internt via lokale netværk uden ekstern adgang.

```mermaid
flowchart TD

    User[Bruger<br/>Browser]

    subgraph Docker Host
        subgraph vlan-web 10.29.30.0/24
            WEBUI[webui-final-stage<br/>ASP.NET WebUI<br/>10.29.30.10<br/>Port 5050]
            WEBAPI[webapi-final-stage<br/>ASP.NET WebAPI<br/>10.29.30.11<br/>Port 5051]
        end

        subgraph vlan-db 10.29.10.0/24
            MYSQL[slottets-sqlserver<br/>MySQL Server<br/>10.29.10.99<br/>Port 3306<br/>Port 3307]
        end
    end

    Data[(Persistent Volume<br/>./Data)]

    User -->|HTTP :5050| WEBUI
    WEBUI -->|HTTP :5051| WEBAPI
    WEBAPI -->|MySQL :3306| MYSQL
    User -->|MySQL :3307| MYSQL
    MYSQL --> Data
```

---


## Alternativt deploymentsdiagram (med proxyserver)

**Scenarie:**
Denne opsætning anvendes til produktion eller testmiljøer, hvor adgang fra internettet styres via en firewall og proxyserver. Firewall håndterer CORS og sikkerhed, mens proxyen videresender trafik til relevante tjenester. MySQL-databasen er isoleret og kun tilgængelig for WebAPI.

```mermaid
flowchart TD

    PublicNet[Offentlig netværk<br/>Bruger]
    subgraph Server Infrastruktur
      FIREWALL[Firewall<br/>Håndterer CORS]
      PROXY[Proxyserver<br/>Nginx<br/>10.29.40.5<br/>Port 80/443]
      WEBUI[webui-final-stage<br/>ASP.NET WebUI<br/>10.29.30.10<br/>Port 5050]
      WEBAPI[webapi-final-stage<br/>ASP.NET WebAPI<br/>10.29.30.11<br/>Port 5051]
      MYSQL[slottets-sqlserver<br/>MySQL Server<br/>10.29.10.99<br/>Port 3306]
    end
      PublicNet -->|HTTPS| FIREWALL
      FIREWALL -->|HTTPS| PROXY
      PROXY -->|HTTP :5050| WEBUI
      PROXY -->|HTTP :5051| WEBAPI
      WEBUI -->|HTTP :5051| WEBAPI
      WEBAPI -->|MySQL :3306| MYSQL
```


### Noter

- Firewall håndterer CORS og begrænser adgang (kan være del af serveren).
- HTTP bruges internt for ydeevne, men HTTPS anbefales hvis netværket ikke er isoleret.
