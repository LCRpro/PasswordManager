# Projet Fil Rouge : Gestionnaire de Mots de Passe

## Auteurs
- **[Liam Cariou]** - Étudiant

## Description
Ce projet vise à développer une solution Blazor pour un gestionnaire de mots de passe, destiné aux étudiants en enseignement supérieur. Il permettra aux utilisateurs de stocker, gérer et accéder à leurs mots de passe en toute sécurité grâce à une interface conviviale.

## 🏁 Démarrage du projet

### 1️⃣ Git clone le projet

git clone https://github.com/LCRpro/PasswordManager

### 1️⃣ Lancer la base de données SQLite
Si la base de données n'existe pas encore, crée-la avec **Entity Framework Core** :

cd src/PasswordManager.Core
dotnet ef database update

### 2️⃣ Lancer l'api

cd src/PasswordManager.Api
dotnet run
http://localhost:5233/swagger/index.html

### 3️⃣ Lancer l'interface Blazor WebAssembly
Dans un autre terminal : 

cd src/PasswordManager.Web
dotnet run
http://localhost:5023



## Fonctionnalités principales
- ✅**Authentification (3pts)** : Inscription, connexion.
- ✅**Gestion des mots de passe (2pts)** : Ajouter, modifier et supprimer des mots de passe.
- ✅**Catégorisation (2pts)** : Organiser les mots de passe par catégorie.
- ✅**Recherche rapide (2pts)** : Trouver facilement des mots de passe via une barre de recherche.
- ❌**Chiffrement (3pts)** : Sécuriser les données avec des algorithmes de chiffrement robustes.
- ✅**Mode hors ligne (2pts)** : Accès local sécurisé sans dépendance réseau.
- ❌**Sécurité (3pts)** : Utilisation d’un mot de passe principal pour la décryptage, verrouillage après tentatives échouées.
- ✅**Générateur de mots de passe (2pts)** : Génération de mots de passe complexes et sécurisés avec critères personnalisables.
- ✅**Sauvegarde (5pts)** : sur une base de données SQLite.
- ✅**Interface utilisateur (4pts)** : Simple, intuitive, et responsive avec recherche et filtrage des mots de passe.
- ✅**Points bonus (5pts)**: Tests unitaires

## Technologies utilisées
- ✅**Framework front-end (5pts)** : [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
- ✅**Backend (2pts)** : ASP.NET Core Web API
- ✅**Base de données** : SQLite
- ✅**Autres (5pts)** : Entity Framework Core, Dependency Injection (services), Stockage Local.