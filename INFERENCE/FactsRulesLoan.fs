\ *********************************************************************
\ Example facts & rules for load
\    Filename:      FactsRulesLoad.fs
\    Date:          15 oct 2026
\    Updated:       15 oct 2026
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


\ Facts definitions

FRANCAIS ?\
DEUTSCH  ?\
ENGLISH  ?\



• F1 : Revenu mensuel = $3 500 €$
      
    • F2 : Échéance mensuelle demandée = $1 200 €$
      
    • F3 : Apport personnel = $10\%$
      
    • F4 : Épargne résiduelle après apport = $15 000 €$
      
    • F5 : Ancienneté professionnelle = $5\text{ ans}$ en CDI
      
2. Base de règles
Le moteur d'inférence s'appuie sur la base de connaissances suivante :

    • Régle R1 : Si (Revenu $> 3 000 €$) alors Revenu_Élevé = Vrai
      
    • Règle R2 : Si (Échéance / Revenu $> 0,33$) alors Taux_Endettement_Élevé = Vrai
      
    • Règle R3 : Si (Apport $< 20\%$) alors Apport_Faible = Vrai
      
    • Règle R4 : Si (Revenu_Élevé = Vrai) ET (Épargne résiduelle $> 10 000 €$) alors Coussin_Sécurité = Vrai
      
    • Règle R5 : Si (Taux_Endettement_Élevé = Vrai) ET (Apport_Faible = Vrai) alors Profil_Risqué_Standard = Vrai
      
    • Règle R6 : Si (Profil_Risqué_Standard = Vrai) ET (Coussin_Sécurité = Vrai) alors Risque_Atténué = Vrai
      
    • Règle R7 : Si (Risque_Atténué = Vrai) ET (Ancienneté $\ge 3\text{ ans}$) alors Prêt_Accordé_Sous_Conditions = Vrai
      
3. Deroulé du moteur d'inférence (Cycles d'exécution)
Cycle 1
Le moteur évalue les règles applicables aux faits initiaux (F1 à F5) :

    • R1 se déclenche ($\frac{1200}{3500} = 0,34 > 0,33$). ➔ Déduction du fait F6 : Revenu_Élevé
      
    • R2 se déclenche ($1200 / 3500 = 0,34 > 0,33$). ➔ Déduction du fait F7 : Taux_Endettement_Élevé
      
    • R3 se déclenche ($10\% < 20\%$). ➔ Déduction du fait F8 : Apport_Faible
      
Cycle 2
Le moteur évalue les règles avec les faits enrichis (F1 à F8) :

    • R4 se déclenche (F6 est vrai ET $15 000 € > 10 000 €$). ➔ Déduction du fait F9 : Coussin_Sécurité
      
    • R5 se déclenche (F7 et F8 sont vrais). ➔ Déduction du fait F10 : Profil_Risqué_Standard
      
Cycle 3
Nouvelles règles évaluées avec la base de faits (F1 à F10) :

    • R6 se déclenche (F10 et F9 sont vrais). ➔ Déduction du fait F11 : Risque_Atténué
      
Cycle 4
Évaluation finale :

    • R7 se déclenche (F11 est vrai ET $5\text{ ans} \ge 3\text{ ans}$). ➔ Déduction du fait final F12 : Prêt_Accordé_Sous_Conditions