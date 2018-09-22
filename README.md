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

### Determine Rxcui
The first step is to determine the RxNorm concept unique identifier (Rxcui). In RxNorm, the name of a clinical drug combines its ingredients, strengths, and/or form.

RxNLizer splits the the medication to tokens, and selects the preferred the Token to match against STR column of the RxNorm Rxnconso table. The preferred token is usually the first word, and is used in the predicate to return a set of matched RxNorm records with the RxCui and STR fields. Each STR field is tokenized

The tokenized Rx_text is matched against the selected tokenized RxNorm matches. The candidate Rxcui is the record with the highest number of token matches and lowest difference in length. 

### Obtain RxClass object through REST API using the Rxcui
The candidate RxCui is then used to query the RxClass REST Api to obtain the relevant RxClass(es) as a Json Object

The rxNLizer.GetRxClasses(rxText) method returns this aforementioned Json Object



