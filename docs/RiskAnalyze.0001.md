# Risikoanalyse
Denne risikoanalyse opsummerer hovedrisici for projektet opdelt efter kategori. Hver risiko har et entydigt ID, en vurdering af sandsynlighed (1-5), alvorlighed (1-5), en beregnet risikoscore (Likelihood × Severity) og anbefalede afhjælpende tiltag.

## Risikoniveau (legend)
| Score | Niveau |
|---:|---|
|15–25 | Kritisk |
|10–14 | Høj |
|5–9 | Moderat |
|1–4 | Lav |

---

## 1) Tekniske risici
Tekniske risici omfatter problemer i systemets arkitektur, kode eller infrastruktur (fx sikkerhedssårbarheder, afhængigheder og databaseforbindelser), som kan føre til datatab, nedetid eller kompromitteret integritet.
### Risiko
| ID | Risiko | Beskrivelse |
|---|---|---|
| R-RT-001 | Sikkerhedssårbarheder | Risiko for datalæk, uautoriseret adgang, manglende kryptering, utilstrækkelig adgangskontrol |
| R-RT-002 | Databrud | Uautoriseret adgang pga. svag autentifikation eller sessionsstyring |
| R-RT-003 | Afhængighed | Sårbarheder i tredjepartsbiblioteker eller frameworks |
| R-RT-004 | Databaseforbindelsesproblemer | Nedetid eller ydeevneproblemer, især ved netværksfejl eller cloud-migration |
| R-RT-005 | Systemintegration | Fejl i integration med eksterne systemer (fx FMK) |
| R-RT-006 | Datahistorik og sporbarhed | Tab af historik eller manglende sporbarhed ved fejl eller forkert brug |

### Risikoniveau
| ID | Likelihood (1-5) | Severity (1-5) | Score | Risiko-niveau |
|---|:---:|:---:|:---:|---|
| R-RT-001 |4 |5 |20 | Kritisk |
| R-RT-002 |4 |4 |16 | Høj |
| R-RT-003 |3 |3 |9 | Moderat |
| R-RT-004 |3 |4 |12 | Høj |
| R-RT-005 |2 |4 |8 | Moderat |
| R-RT-006 |2 |5 |10 | Høj |

### Mitigation
<table border="1" width="100%" style="border-collapse: collapse;">
  <tr><th>ID</th><th>Mitigation</th></tr>
  <tr><td>R-RT-001</td><td><ul>
    <li>Stærk adgangskontrol og kryptering af følsomme data</li>
    <li>Regelmæssig sikkerhedstest og opdatering af software</li>
    <li>Implementer GDPR-compliance og audit-logs</li>
  </ul></td></tr>
  <tr><td>R-RT-002</td><td><ul>
    <li>Stærk autentifikation (fx 2-faktor)</li>
    <li>Session timeout og overvågning</li>
  </ul></td></tr>
  <tr><td>R-RT-003</td><td><ul>
    <li>Hold afhængigheder opdaterede</li>
    <li>Brug værktøjer til at scanne for sårbarheder</li>
  </ul></td></tr>
  <tr><td>R-RT-004</td><td><ul>
    <li>Implementer failover og backup-strategier</li>
    <li>Overvågning af databaseforbindelser</li>
  </ul></td></tr>
  <tr><td>R-RT-005</td><td><ul>
    <li>Test integrationer grundigt</li>
    <li>Implementer fallback og fejlhåndtering</li>
  </ul></td></tr>
  <tr><td>R-RT-006</td><td><ul>
    <li>Automatisk backup og versionering af data</li>
    <li>Audit-log for alle ændringer</li>
  </ul></td></tr>
</table>

---


## 2) Operationelle risici
Operationelle risici vedrører interne processer, personale og driftsprocedurer (fx dårlig UI/UX, mangelfuld dokumentation eller utilstrækkelig support), som kan påvirke adoption, kvalitet og leverancer.

### Risiko
| ID | Risiko | Beskrivelse |
|---|---|---|
| R-RO-001 | Brugeroplevelse | Dårligt UI/UX kan føre til lav adoption, fejl i vagtskifte og manglende overblik |
| R-RO-002 | Dokumentation | Mangelfuld dokumentation kan skabe forvirring og fejl |
| R-RO-003 | Support | Utilstrækkelig support kan føre til utilfredse brugere |
| R-RO-004 | Fejl i dataindtastning | Manuelle fejl ved indtastning af borgerdata eller medicin |
| R-RO-005 | Manglende brugerinvolvering | Systemet matcher ikke arbejdsgange, hvis brugerne ikke inddrages i design og test |

### Risikoniveau
| ID | Likelihood (1-5) | Severity (1-5) | Score | Risiko-niveau |
|---|:---:|:---:|:---:|---|
| R-RO-001 |3 |4 |12 | Høj |
| R-RO-002 |2 |4 |8 | Moderat |
| R-RO-003 |2 |3 |6 | Moderat |
| R-RO-004 |3 |4 |12 | Høj |
| R-RO-005 |3 |4 |12 | Høj |

### Mitigation
<table border="1" width="100%" style="border-collapse: collapse;">
  <tr><th>ID</th><th>Mitigation</th></tr>
  <tr><td>R-RO-001</td><td><ul>
    <li>Involver brugere i design og test</li>
    <li>Iterer på UI/UX baseret på feedback</li>
    <li>Responsivt og intuitivt design</li>
  </ul></td></tr>
  <tr><td>R-RO-002</td><td><ul>
    <li>Udarbejd og vedligehold brugerdokumentation</li>
    <li>Gør dokumentation let tilgængelig</li>
  </ul></td></tr>
  <tr><td>R-RO-003</td><td><ul>
    <li>Tilbyd dedikeret support og træning</li>
    <li>Implementer ticketing-system</li>
  </ul></td></tr>
  <tr><td>R-RO-004</td><td><ul>
    <li>Validering af input og brug af dropdowns/afkrydsningsfelter</li>
    <li>Automatiske påmindelser og fejlmeddelelser</li>
  </ul></td></tr>
  <tr><td>R-RO-005</td><td><ul>
    <li>Brugerinvolvering i alle faser</li>
    <li>Workshops og feedbackrunder</li>
  </ul></td></tr>
</table>

---


## 3) Projektledelsesrisici
Projektledelsesrisici omfatter faktorer som tidsplanoverskridelser, budgetoverskridelser og ressourceallokering, som kan påvirke projektets succes.
### Risiko
| ID | Risiko | Beskrivelse |
|---|---|---|
| R-PM-001 | Tidsplanoverskridelser | Forsinkelser i projektleverancer |
| R-PM-002 | Budgetoverskridelser | Uventede omkostninger, der overskrider budgettet |
| R-PM-003 | Ressourceallokering | Utilstrækkelige ressourcer til at fuldføre opgaver |
| R-PM-004 | Sygdom/manglende kompetence | Nøglepersoner bliver syge eller mangler nødvendige kompetencer |
| R-PM-005 | Modstand mod forandring | Personalet foretrækker papir og gamle rutiner frem for digital løsning |

### Risikoniveau
| ID | Likelihood (1-5) | Severity (1-5) | Score | Risiko-niveau |
|---|:---:|:---:|:---:|---|
| R-PM-001 |3 |4 |12 | Høj |
| R-PM-002 |2 |5 |10 | Høj |
| R-PM-003 |3 |3 |9 | Moderat |
| R-PM-004 |2 |4 |8 | Moderat |
| R-PM-005 |3 |4 |12 | Høj |

### Mitigation
<table border="1" width="100%" style="border-collapse: collapse;">
  <tr><th>ID</th><th>Mitigation</th></tr>
  <tr><td>R-PM-001</td><td><ul>
    <li>Opret realistiske tidsplaner</li>
    <li>Overvåg fremskridt regelmæssigt</li>
    <li>Juster planer efter behov</li>
  </ul></td></tr>
  <tr><td>R-PM-002</td><td><ul>
    <li>Opret detaljeret budget</li>
    <li>Overvåg omkostninger løbende</li>
    <li>Identificer potentielle besparelser</li>
  </ul></td></tr>
  <tr><td>R-PM-003</td><td><ul>
    <li>Sørg for tilstrækkelige ressourcer</li>
    <li>Omfordel efter behov</li>
    <li>Ansæt midlertidigt personale ved spidsbelastninger</li>
  </ul></td></tr>
  <tr><td>R-PM-004</td><td><ul>
    <li>Sørg for backup-ressourcer</li>
    <li>Kompetenceudvikling</li>
    <li>Vidensdeling til kollegaer</li>
    <li>God kode og dokumentation</li>
    <li>Fleksibel ressourceallokering</li>
  </ul></td></tr>
  <tr><td>R-PM-005</td><td><ul>
    <li>Involver personalet tidligt i processen</li>
    <li>Tilbyd træning og støtte</li>
    <li>Kommuniker fordelene ved digitalisering</li>
  </ul></td></tr>
</table>

---


## 4) Eksterne risici
Eksterne risici inkluderer faktorer uden for projektets kontrol, såsom ændringer i lovgivning, markedsforhold eller leverandørproblemer.

### Risiko
| ID | Risiko | Beskrivelse |
|---|---|---|
| R-EX-001 | Lovgivningsændringer | Nye regler, der påvirker projektets gennemførelse |
| R-EX-002 | Markedsforhold | Ændringer i markedet, der påvirker projektets relevans |
| R-EX-003 | Leverandørproblemer | Forsinkelser eller problemer med tredjepartsleverandører |
| R-EX-004 | Netværks- og strømudfald | System utilgængeligt pga. netværks- eller strømproblemer |

### Risikoniveau
| ID | Likelihood (1-5) | Severity (1-5) | Score | Risiko-niveau |
|---|:---:|:---:|:---:|---|
| R-EX-001 |2 |4 |8 | Moderat |
| R-EX-002 |3 |3 |9 | Moderat |
| R-EX-003 |2 |3 |6 | Moderat |
| R-EX-004 |2 |5 |10 | Høj |

### Mitigation
<table border="1" width="100%" style="border-collapse: collapse;">
  <tr><th>ID</th><th>Mitigation</th></tr>
  <tr><td>R-EX-001</td><td><ul>
    <li>Hold dig opdateret om lovgivningsændringer</li>
    <li>Konsulter juridiske eksperter</li>
    <li>Tilpas projektplaner efter behov</li>
  </ul></td></tr>
  <tr><td>R-EX-002</td><td><ul>
    <li>Overvåg markedstendenser</li>
    <li>Juster projektets fokus for at forblive relevant</li>
    <li>Involver interessenter</li>
  </ul></td></tr>
  <tr><td>R-EX-003</td><td><ul>
    <li>Vælg pålidelige leverandører</li>
    <li>Opret backup-planer</li>
    <li>Overvåg leverandørpræstationer løbende</li>
  </ul></td></tr>
  <tr><td>R-EX-004</td><td><ul>
    <li>Implementer offline-funktionalitet hvor muligt</li>
    <li>Brug UPS og redundant netværk</li>
    <li>Informer om nødprocedurer ved nedbrud</li>
  </ul></td></tr>
</table>
