using ProjetLinq.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPLesAuteurs
{
    class Program
    {
        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        static void Main(string[] args)
        {


            InitialiserDatas();

            //Afficher la liste des prénoms des auteurs dont le nom commence par "G"
            var listePrenomG = ListeAuteurs.Where(n => n.Nom.StartsWith("G"));
            foreach (var auteurG in listePrenomG)
            {
                Console.WriteLine( "Prénom : " + auteurG.Prenom);
            }

            Console.WriteLine();
            
            //Afficher l’auteur ayant écrit le plus de livres
            Auteur auteurPlusLivre = null;
            int auteurPlusLivreNb = 0;

            foreach (var aute in ListeAuteurs)
            {
                if (ListeLivres.Count(x => x.Auteur == aute) > auteurPlusLivreNb)
                {
                    auteurPlusLivre = aute;
                    auteurPlusLivreNb = ListeLivres.Count(x => x.Auteur == aute);
                }
            }
            Console.WriteLine($" Auteur nom : {auteurPlusLivre.Nom} , prenom : {auteurPlusLivre.Prenom}, nb livre : {auteurPlusLivreNb}");


            //Afficher le nombre moyen de pages par livre par auteur

            foreach(var at in ListeAuteurs)
            {
                var totLivre = ListeLivres.Count(n => n.Auteur == at);
                var totPages = ListeLivres.Where(n => n.Auteur == at).Sum(n => n.NbPages);
                if(totLivre != 0)
                {
                    Console.WriteLine($"Pour {at.Nom} {at.Prenom} on a une moyenne de {totPages / totLivre}");

                }
            }


            //Afficher le titre du livre avec le plus de pages
            var titre = ListeLivres.OrderBy(p => p.NbPages);
            Console.WriteLine("Titre : " +titre.First().Titre);

            //Afficher combien ont gagné les auteurs en moyenne (moyenne des factures)
            var moyenne = ListeAuteurs.Average(a => a.Factures.Sum(f => f.Montant));
            Console.WriteLine("Combien ont gagné les auteurs en moyenne");
            Console.WriteLine(moyenne);

            //Afficher les auteurs et la liste de leurs livres

            foreach (var aut in ListeAuteurs)
            {
                Console.WriteLine($"Auteur {aut.Nom} {aut.Prenom}");
                foreach(var livre in ListeLivres)
                {
                    if(livre.Auteur == aut)
                    {
                        Console.WriteLine($"{livre.Titre}");
                    }
                }
            }

            //Afficher les titres de tous les livres triés par ordre alphabétique
            var titreAsc = ListeLivres.OrderBy(n => n.Titre);
            foreach(var titreA in titreAsc)
            {
                Console.WriteLine(titreA.Titre);
            }

            //Afficher la liste des livres dont le nombre de pages est supérieur à la moyenne
            var nbLivre = ListeLivres.Count();
            var totPage = ListeLivres.Sum(n => n.NbPages);
            var moy = totPage / nbLivre;
            var listeLivrePagesSup = ListeLivres.Where(n => n.NbPages > moy);
            Console.WriteLine("liste des livres dont le nombre de pages est supérieur à la moyenne");
            foreach (var l in listeLivrePagesSup)
            {
                Console.WriteLine(l.Titre);
            }


            //Afficher l'auteur ayant écrit le moins de livres
            Auteur auteurMoinsLivre = null;
            int auteurMoisLivreNb = 100;

            foreach (var aute in ListeAuteurs)
            {
                if (ListeLivres.Count(x => x.Auteur == aute) < auteurPlusLivreNb)
                {
                    auteurMoinsLivre = aute;
                    auteurMoisLivreNb = ListeLivres.Count(x => x.Auteur == aute);
                }
            }
            Console.WriteLine($" Auteur nom : {auteurMoinsLivre.Nom} , prenom : {auteurMoinsLivre.Prenom}, nb livre : {auteurMoisLivreNb}");


            Console.ReadKey();

        }
        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }
    }
}
