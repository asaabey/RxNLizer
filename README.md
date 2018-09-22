# RxNLizer
Normalize and classify using RxNorm and RxClass

Example walkthrough

Prerequisite
RxNorm SQL database installed locally. Source can be downloaded from 
https://www.nlm.nih.gov/research/umls/rxnorm/docs/rxnormfiles.html

What are the RxClass codes for medication string containing, "Amlodipine 10 mg, Atorvastatin 80 mg coated tablet; 10 mg, 80 mg 10 mg/80 mg" ?

The first step is to determine the RxNorm concept unique identifier (RXCUI).In RxNorm, the name of a clinical drug combines its ingredients, strengths, and/or form.

RxNLizer splits the the medication to tokens, and selects the preferred the Token to match against STR column of the RxNorm Rxnconso table
The preferred token is usually the first word, and is used in the predicate to return a set of matched RxNorm records with the RxCui and STR fields.



