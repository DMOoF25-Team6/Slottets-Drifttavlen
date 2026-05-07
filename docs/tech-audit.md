# Tech Audit

<table>
  <tr>
    <th>Emne</th>
    <th>Hvor i projektet</th>
    <th>Valgte teknologier</th>
    <th>Hvorfor</th>
    <th>Hvad gør vi ikke</th>
  </tr>
  <tr>
    <td>Netværk (OSI/TCP-IP,protokoller)</td>
    <td>
      <ul>
        <li>Backend API (HTTP/HTTPS via REST)</li>
        <li>Blazor frontend (HTTP/HTTPS, WebSockets/SignalR)</li>
        <li>Kommunikation mellem microservices (HTTP/HTTPS)</li>
        <li>Dashboard-opdateringer (WebSockets/SignalR)</li>
      </ul>
    </td>
    <td>
      <ul>
        <li>TCP/IP</li>
        <li>HTTP/HTTPS</li>
        <li>WebSockets</li>
      </ul>
    </td>
    <td>
      TCP/IP er grundlaget for internettet, og HTTP/HTTPS er de mest udbredte protokoller til webkommunikation. WebSockets for opdatering til dashboard og Blazor (SignalR).
    </td>
    <td>
      Vi bruger ikke ældre protokoller som FTP eller SMTP, da de ikke er relevante for vores applikation.
    </td>
  </tr>
  <tr>
    <td>Distributed Systems (arkitekturmønster)</td>
    <td>
      <ul>
        <li>Microservices i backend (Application/Infrastructure lag)</li>
        <li>Kommunikation mellem services via RESTful APIs</li>
        <li>Evt. API-gateway (hvis relevant)</li>
        <li>Deployment i containere (Docker Compose)</li>
      </ul>
    </td>
    <td>
      <ul>
        <li>Microservices</li>
        <li>RESTful APIs</li>
        <li>Docker</li>
        <li>Clean Architecture</li>
      </ul>
    </td>
    <td>
      Microservices-arkitektur giver fleksibilitet, skalerbarhed og mulighed for uafhængig deployment. RESTful APIs muliggør standardiseret kommunikation mellem tjenester. Docker sikrer konsistente miljøer og nem deployment. Clean Architecture adskiller forretningslogik fra infrastruktur.
    </td>
    <td>
      Vi bruger ikke monolitiske arkitekturer eller "tightly coupled" services, da de er mindre fleksible og sværere at skalere og vedligeholde.
    </td>
  </tr>
  <tr>
    <td>Operativsystemer (processer,filsystem)</td>
    <td>
      <ul>
        <li>Backend og API-hosting (Linux-server i produktion, Windows og Linux til udvikling)</li>
        <li>Filsystemadgang i Infrastructure-laget (logning, datafiler, konfiguration)</li>
        <li>Udviklingsmiljøer (Visual Studio på Windows, CLI på Linux)</li>
        <li>Docker-containere (baseret på Linux-images)</li>
      </ul>
    </td>
    <td>
      <ul>
        <li>Windows</li>
        <li>Linux</li>
        <li>Docker</li>
      </ul>
    </td>
    <td>
      Vi understøtter både Windows og Linux for at sikre fleksibilitet i udvikling og produktion. Linux foretrækkes til serverdrift og container-miljøer, mens Windows ofte bruges til udvikling (Visual Studio). Docker sikrer ensartede miljøer på tværs af OS.
    </td>
    <td>
      Vi undgår mindre udbredte operativsystemer og ikke-understøttede platforme, da de kan give kompatibilitetsproblemer og manglende support.
    </td>
  </tr>
  <tr>
    <td>Virtualisering / Docker</td>
    <td>
      <ul>
        <li>Containerisering af backend- og API-services (Dockerfile i src/)</li>
        <li>Lokal udvikling og test (docker-compose.yml i roden)</li>
        <li>Deployment til test- og produktionsmiljøer (Docker Compose, evt. cloud)</li>
        <li>CI/CD pipelines (bygning og test af images)</li>
      </ul>
    </td>
    <td>
      <ul>
        <li>Docker</li>
        <li>Docker Compose</li>
        <li>CI/CD integration</li>
      </ul>
    </td>
    <td>
      Docker sikrer ensartede og isolerede miljøer til udvikling, test og produktion. Docker Compose gør det nemt at orkestrere flere services lokalt og i test. CI/CD pipelines automatiserer bygning, test og deployment af containere.
    </td>
    <td>
      Vi bruger ikke fuld virtualisering (f.eks. VMware) eller manuelle miljøopsætninger, da de giver mere overhead, kompleksitet og risiko for miljøafvigelser.
    </td>
  </tr>
  <tr>
    <td>Sikkerhed (mindst 3 OWASPkategorier)</td>
    <td>
      <ul>
        <li>Backend API (validering, autentificering, autorisation, input-sanitization)</li>
        <li>Blazor frontend (beskyttelse mod XSS, håndtering af følsomme data)</li>
        <li>Databaseadgang (beskyttelse mod SQL Injection)</li>
        <li>Kommunikation (HTTPS, beskyttelse af data i transit)</li>
        <li>CI/CD pipelines (afhængighedsscanning, secrets management)</li>
      </ul>
    </td>
    <td>
      <ul>
        <li>OWASP Top 10 (Injection, Broken Authentication, Sensitive Data Exposure, XSS)</li>
        <li>HTTPS</li>
        <li>Identity Management</li>
        <li>Secrets management i CI/CD</li>
      </ul>
    </td>
    <td>
      Vi implementerer sikkerhedsforanstaltninger for at beskytte mod de mest almindelige og kritiske sårbarheder, herunder inputvalidering, korrekt håndtering af autentificering og autorisation, kryptering af følsomme data og brug af sikre kommunikationsprotokoller. CI/CD pipelines scanner for sårbarheder og beskytter secrets.
    </td>
    <td>
      Vi undgår at ignorere sikkerhedsprincipper, at hardcode secrets, eller at implementere sikkerhedsforanstaltninger, der ikke er relevante for vores applikation.
    </td>
  </tr>
  <tr>
    <td>CI/CD</td>
    <td>
      <ul>
        <li>Byg, test og deployment af backend og frontend via GitHub Actions workflows</li>
        <li>Docker image build og push til container registry</li>
        <li>Automatisk kørsel af tests ved pull requests og merges</li>
        <li>Deployment til test- og produktionsmiljøer (Docker Compose eller cloud)</li>
      </ul>
    </td>
    <td>
      <ul>
        <li>GitHub Actions</li>
        <li>Docker</li>
        <li>Docker Compose</li>
        <li>Container registry</li>
      </ul>
    </td>
    <td>
      CI/CD automatiserer hele processen fra build og test til deployment, hvilket sikrer hurtig feedback, konsistente miljøer og reducerer risikoen for fejl. Docker og container registry muliggør nem distribution og versionering af images.
    </td>
    <td>
      Vi undgår manuelle deployment-processer og uautomatiserede builds/tests, da de øger risikoen for fejl, inkonsistens og ineffektivitet.
    </td>
  </tr>
</table>
