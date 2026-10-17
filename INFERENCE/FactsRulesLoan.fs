\ *********************************************************************
\ Example facts & rules for load
\    Filename:      FactsRulesLoan.fs
\                     see FactsRulesLoan_doc.txt for documentation (in french)
\    Date:          15 oct 2026
\    Updated:       16 oct 2026
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



\ Facteur d'échelle pour la précision (1 000 000 = 1.0)
1000000 CONSTANT PRECISION

\ Mot pour calculer la puissance (1+r)^N en entiers avec virgule fixe
: POWER-FIXED { base n -- result }
    PRECISION 
    n 0 ?do
        base * PRECISION /
    loop
;

\ Calcul exact de la mensualité (64 bits)
\ Pile : ( capital annees taux_annuel -- mensualite )
: MENSUALITE-EXACTE { capital annees taux_annuel -- mensualite }
    \ 1. Calcul du taux mensuel r en virgule fixe
    taux_annuel PRECISION * 120000 / { r }
    
    \ 2. Calcul de (1 + r)^N
    PRECISION r + annees 12 * POWER-FIXED { num_pow }
    
    \ 3. Numerateur corrigé (une seule division par PRECISION pour garder la bonne échelle)
    capital r * num_pow * PRECISION / { num }
    
    \ 4. Denominateur = num_pow - PRECISION
    num_pow PRECISION - { den }
    
    \ 5. Résultat final = Numerateur / Denominateur
    num den /
;

\ Test avec vos valeurs : 210000 € sur 20 ans à 3.5% (350)
\ 210000 20 350 MENSUALITE-EXACTE .


\ *** Facts and Rules definitions *************************************

en-francais \ select french language


FRANCAIS ?\ z" Montant à emprunter"
DEUTSCH  ?\ z" Darlehensbetrag"
ENGLISH  ?\ z" Amount to be borrowed"
210000 FACT: loanAmount

FRANCAIS ?\ z" Durée du pret (années)"
DEUTSCH  ?\ z" Laufzeit des Darlehens (Jahre)"
ENGLISH  ?\ z" Loan term (years)"
20 FACT: loanTerms

FRANCAIS ?\ z" Taux du prêt (% x 100)"
DEUTSCH  ?\ z" Darlehenszins (% x 100)"
ENGLISH  ?\ z" Loan interest rate (% x 100)"
350 FACT: interestRate

FRANCAIS ?\ z" Échéance mensuelle demandée"
DEUTSCH  ?\ z" Gewünschte monatliche Rate"
ENGLISH  ?\ z" Requested monthly installment"
FALSE FACT: monthlyInstallment

:CONDITION loanAmount 0 >  loanTerms 0 > and interestRate 0 > and ;
:ACTION    loanAmount loanTerms interestRate MENSUALITE-EXACTE to monthlyInstallment ;
RULE: calcMonthlyInstallment

FRANCAIS ?\ z" Revenu mensuel net après impôts"
DEUTSCH  ?\ z" Monatliches Nettoeinkommen nach Steuern"
ENGLISH  ?\ z" Net monthly income after taxes"
2400 FACT: monthIncome

FRANCAIS ?\ z" Revenu élevé"
DEUTSCH  ?\ z" Hohes Einkommen"
ENGLISH  ?\ z" High income"
FALSE FACT: hightIncome

:CONDITION hightIncome 3000 > ;
:ACTION    TRUE to hightIncome ;
RULE: calcMonthlyInstallment

FRANCAIS ?\ z" Taux d'endettement élevé"
DEUTSCH  ?\ z" Hoher Verschuldungsgrad"
ENGLISH  ?\ z" High debt-to-income ratio"
FALSE FACT: highDebtToIncome

:CONDITION monthlyInstallment 100 * monthIncome / 33 > ;
:ACTION    TRUE to highDebtToIncome ;
RULE: hightDebtTest


FRANCAIS ?\ z" Ancienneté professionnelle"
DEUTSCH  ?\ z" Berufliche Betriebszugehörigkeit"
ENGLISH  ?\ z" Personal contribution"
4 FACT: personalContrib

FRANCAIS ?\ z" Apport personnel"
DEUTSCH  ?\ z" Eigenkapital"
ENGLISH  ?\ z" Apport personnel"
FALSE FACT: profSeniority

\     • F4 : Épargne résiduelle après apport = $15 000 €$
      
