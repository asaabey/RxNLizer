# RxNLizer
Normalize and classify a medication string using RxNorm and RxClass

## Example walkthrough

Prerequisite

(Public domain) : RxNorm SQL database installed locally. Source can be downloaded from 
https://www.nlm.nih.gov/research/umls/rxnorm/docs/rxnormfiles.html

(Public domain) : RxClass REST Api
https://rxnav.nlm.nih.gov/RxClassAPIREST.html

### Source medication string (Rx_text)
What are the RxClass codes for medication string (Rx_text) containing, "Amlodipine 10 mg, Atorvastatin 80 mg coated tablet; 10 mg, 80 mg 10 mg/80 mg" ? 

### Determine preferred Token
RxNLizer splits the the medication to tokens, and selects the preferred the Token to match against STR column of the RxNorm Rxnconso table. The preferred token is usually the first word, and is used in the predicate to return a set of matched RxNorm records with the RxCui and STR fields. Each STR field is tokenized


### Select matches from RxNorm

    rxcui	str
    1050803	ALISKIREN 300 MG / AMLODIPINE 10 MG / HCTZ 12.5 MG ORAL TABLET
    1152278	AMLODIPINE / HYDROCHLOROTHIAZIDE / OLMESARTAN PILL
    1152281	AMLODIPINE / ATORVASTATIN ORAL PRODUCT
    1600728	AMLODIPINE 5 MG / PERINDOPRIL ARGININE 7 MG ORAL TABLET
    2047714	AMLODIPINE / CELECOXIB
    2047715	AMLODIPINE 2.5 MG (AS AMLODIPINE BESYLATE 3.47 MG) / CELECOXIB 200 MG ORAL TABLET
    ... (Ommitted)
    999967	OLMESARTAN MEDOXOMIL 20 MG / AMLODIPINE 5 MG / HYDROCHLOROTHIAZIDE 12.5 MG ORAL TABLET
    (354 rows) returned

### Determine Rxcui
The first step is to determine the RxNorm concept unique identifier (Rxcui). In RxNorm, the name of a clinical drug combines its ingredients, strengths, and/or form.

The tokenized Rx_text is matched against the selected tokenized RxNorm matches. The candidate Rxcui is the record with the highest number of token matches and lowest difference in length. 

    rxcui	str
    404013	AMLODIPINE (AS AMLODIPINE BESYLATE) 10 MG / ATORVASTATIN (AS ATORVASTATIN CALCIUM) 80 MG ORAL TABLET
    404013	AMLODIPINE 10 MG / ATORVASTATIN 80 MG ORAL TABLET
    404013	AMLODIPINE BESYLATE 10 MG / ATORVASTATIN CALCIUM 80 MG ORAL TABLET
    404013	AMLODIPINE BESYLATE 10 MG / ATORVASTATIN CALCIUM 80 MG ORAL TABLET, FILM COATED
    404013	AMLODIPINE BESYLATE 10 MG / ATORVASTATIN CALCIUM TRIHYDRATE 80 MG ORAL TABLET, FILM COATED
    404013	AMLODIPINE BESYLATE 10 MG / ATORVASTATIN CALCIUM TRIHYDRATE 80 MG ORAL TABLET, FILM COATED [AMLODIPINE BESYLATE AND ATORVASTATIN CALCIUM]

### Obtain RxClass object through REST API using the Rxcui
The candidate RxCui is then used to query the RxClass REST Api to obtain the relevant RxClass(es) as a Json Object. In this example the class type has ben restricted to ATC1-4.

The rxNLizer.GetRxClasses(rxText) method returns this aforementioned Json Object

      {
      "userInput": {
        "relaSource": "ATC",
        "relas": "ALL",
        "rxcui": "404013"
      },
      "rxclassDrugInfoList": {
        "rxclassDrugInfo": [
          {
            "minConcept": {
              "rxcui": "17767",
              "name": "Amlodipine",
              "tty": "IN"
            },
            "rxclassMinConceptItem": {
              "classId": "C08CA",
              "className": "Dihydropyridine derivatives",
              "classType": "ATC1-4"
            },
            "rela": "",
            "relaSource": "ATC"
          },
          {
            "minConcept": {
              "rxcui": "404773",
              "name": "Amlodipine / atorvastatin",
              "tty": "MIN"
            },
            "rxclassMinConceptItem": {
              "classId": "C10BX",
              "className": "HMG CoA reductase inhibitors, other combinations",
              "classType": "ATC1-4"
            },
            "rela": "",
            "relaSource": "ATC"
          },
          {
            "minConcept": {
              "rxcui": "83367",
              "name": "atorvastatin",
              "tty": "IN"
            },
            "rxclassMinConceptItem": {
              "classId": "C10AA",
              "className": "HMG CoA reductase inhibitors",
              "classType": "ATC1-4"
            },
            "rela": "",
            "relaSource": "ATC"
          }
        ]
      }
    }



