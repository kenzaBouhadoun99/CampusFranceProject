Feature: Création d'un compte Campus France

  En tant qu'utilisateur de Campus France
  Je souhaite créer un compte
  Afin d'accéder à mon espace Campus France

  Background:
    Given je suis sur la page Créer un nouveau compte "https://www.campusfrance.org/fr/user/register" de Campus France


  Scenario Outline: Création d'un compte Chercheur

    When je renseigne les informations personnelles du chercheur "<civilite>", "<nom>", "<prenom>"
    And je renseigne les informations de résidence du chercheur "<pays_residence>", "<nationalite>"
    And je renseigne les coordonnées du chercheur "<code_postal>", "<ville>", "<telephone>"
    And je renseigne les informations de connexion du chercheur "<email>", "<mot_de_passe>", "<confirmation_mot_de_passe>"
    And je sélectionne le profil de chercheur "Chercheur"
    And je renseigne les informations du chercheur "<domaine>", "<niveau_etudes>"

    Then le nom renseigné est "<nom>" pour chercheur


    Examples:
      | civilite | nom       | prenom | pays_residence | nationalite | code_postal | ville   | telephone  | email                        | mot_de_passe | confirmation_mot_de_passe | domaine      | niveau_etudes |
      | Madame   | Bouhadoun | Kenza   | France         | Algérienne  | 95220       | Herblay | 0612345678 | kenza.chercheur@example.com | cher@123456  | cher@123456                | Informatique | Licence 1      |


  Scenario Outline: Création d'un compte Institutionnel

    When je renseigne les informations personnelles de institutionnel "<civilite>", "<nom>", "<prenom>"
    And je renseigne les informations de résidence de institutionnel "<pays_residence>", "<nationalite>"
    And je renseigne les coordonnées de institutionnel "<code_postal>", "<ville>", "<telephone>"
    And je renseigne les informations de connexion de institutionnel "<email>", "<mot_de_passe>", "<confirmation_mot_de_passe>"
    And je sélectionne le profil institutionnel "Institutionnel"
    And je renseigne les informations de l'institutionnel "<fonction>", "<type_organisme>", "<nom_organisme>"

    Then le nom renseigné est "<nom>" pour institutionnel


    Examples:
      | civilite | nom    | prenom | pays_residence | nationalite | code_postal | ville     | telephone  | email                       | mot_de_passe | confirmation_mot_de_passe | fonction                   | type_organisme | nom_organisme        |
      | Monsieur | Martin | Thomas | Gabon       | Algérienne   | 1000        | Bruxelles | 0470123456 | thomas.martin@example.com | insti@456789  | insti@456789                | Directeur des partenariats | Campus France          | École Polytechnique |