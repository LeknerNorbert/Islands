# Islanders backend repo

## A frontend szükséges a teljes program működéséhez, ami egy másik repositoryban van.

[Frontend repository elérése](https://github.com/fireball90/island_base)

## A projekt indításához szükséges feltételek

A projekt indításához az alábbi követelmények szükségesek:
- Visual studio (lehetőleg 2022-es verzió)
- Xampp (adatbázis indításához)
- A projekt fileok klónozása vagy kicsomagolva letöltése

## A projekt indítása

1. Elindítjuk a Xampp nevű programot, majd az Apache és a Mysql felirat melletti start gombbal elindítjuk a szervert és az adatbázist.
2. A mysql sorában az Admin gombra kattitunk, ami megnyit egy ablakot az adatbázis felületével. Vagy a böngésző sávjába bemásoljuk az alábbi szöveget: [http://localhost/phpmyadmin/](http://localhost/phpmyadmin/)
3. Létrehozunk egy új adatbázist Island néven, utf-8 hungarian kódolással.
4. Megnyitjuk az Islands.sln-t Visual studioban.
5. Packet manager console-ban beírjuk, hogy ### `update-database`.
6. Ezután visszatérünk a mysql adatbázishoz és beolvassuk a repository-ban található sql file-t, hogy feltöltsük ellenfelekkel az adatbázist.
7. Elindítjuk a backendet és megnyílik egy új ablakban a swagger az apikkal.



