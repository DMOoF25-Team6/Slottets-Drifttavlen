# Deployment Diagram

## køre på lokalt maskine som monolitisk applikation

```mermaid
flowchart TD

    User[Bruger / Browser]

    subgraph Docker Host
        subgraph vlan-web 10.29.30.0/24
            WEBUI[webui-final-stage<br/>ASP.NET WebUI<br/>10.29.30.10<br/>Port 5050]
            WEBAPI[webapi-final-stage<br/>ASP.NET WebAPI<br/>10.29.30.11<br/>Port 5051]
        end

        subgraph vlan-db 10.29.10.0/24
            MYSQL[slottets-sqlserver<br/>MySQL Server<br/>10.29.10.99<br/>Port 3306]
        end

        Data[(Persistent Volume<br/>./Data)]
    end

    User -->|HTTP :5050| WEBUI

    WEBUI -->|HTTP API Calls :5051| WEBAPI

    WEBAPI -->|MySQL Connection :3306| MYSQL

    MYSQL --> Data
```

---

## Alternative Deployment Diagram (with Proxy Server)

```mermaid
flowchart TD

    PublicNet[Public Network / User]
    subgraph Server Infrastructure
      FIREWALL[Firewall<br/>Handles CORS]
      PROXY[Proxy Server<br/>Nginx<br/>10.29.40.5<br/>Ports 80/443]
      WEBUI[webui-final-stage<br/>ASP.NET WebUI<br/>10.29.30.10<br/>Port 5050]
      WEBAPI[webapi-final-stage<br/>ASP.NET WebAPI<br/>10.29.30.11<br/>Port 5051]
      MYSQL[slottets-sqlserver<br/>MySQL Server<br/>10.29.10.99<br/>Port 3306]
    end
      PublicNet -->|HTTPS| FIREWALL
      FIREWALL -->|HTTPS| PROXY
      PROXY -->|HTTP :5050| WEBUI
      PROXY -->|HTTP :5051| WEBAPI
      WEBUI -->|HTTP API Calls :5051| WEBAPI
      WEBAPI -->|MySQL Connection :3306| MYSQL
```

### Noter

- Firewall for Web (kan være en del af serveren)
  - håndtere cors
  - håndtere begrænsninger for egress og ingress
