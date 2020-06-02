# Kelime Oyunu

## Projenin Amacı
[Words of Wonders](https://play.google.com/store/apps/details?id=com.fugo.wow&hl=en) oyununun bir benzerini yapmak.

## Oyun Seviyeleri ve Bölümleri
Oyun 3 seviyeden oluşuyor.
- 1.seviyede 3 harften oluşan kelimeler,
- 2.seviyede 4 harften oluşan kelimeler,
- 3.seviyede 5 ve üzeri harften oluşan kelimeler bulunuyor.

**Her seviyenin kendine ait 6 tane bölümü var. Oyunda toplamda 18 tane bulmaca bulunmakta.** <br>
**Seviyelerdeki bölümler oyuncuya rastgele sırada verilir.** <br>
**Ekranın alt kısmında bulunan, kelime oluşturmak için kullanılan harfler rastgele bir sırada yerleştirilir. Yani harf butonları bölümler yüklendiğinde dinamik olarak yerleşir.** <br>

## Puanlama
Her doğru tahmin için harf başına oyuncuya 5 puan eklenir. Yanlış tahmin için her harf başına -1 puan verilir. Bölüm bittiğinde geçen zamanın yarısı eksi puan olarak eklenir ve seçilen seviyedeki, çözülen bölüm için toplam puan oluşur.

## Harf Yerini Değiştirme Butonu
Butona basıldığında tahmin etmede kullanılan harf butonlarının yeri değişir.

## Kelime Tahmin Etme
Herhangi bir harf butonuna basıldığında parmağı kaldırmadan diğer harfe gidilerek tahmin kelimesi oluşturulur. Parmağı kaldırdıktan sonra tahminin doğru olup olmadığı kontrol edilir.

## Proje Dökümanı
[Döküman](https://www.dropbox.com/s/ttixepac5u4g4ta/YAZLAB2_PROJE2.pdf?dl=0)

## Proje APK Linki
[APK](https://www.dropbox.com/s/2fm0dh7xluepwid/yazlab3_2.apk?dl=0)

## Ekran Görüntüleri
###### Ana Menü
![menu1](ss/menu.png)
<br>

###### Puan Tablosu
![scoreBoard](ss/scoreBoard.png)
<br>

###### Seviye Menüsü
![menu2](ss/menu2.png)
<br>

###### 1.Seviye Bulmaca
![puzz](ss/puzz.png)
<br>

###### 2.Seviye Bulmaca
![puzz](ss/puzz2.png)
<br>

###### 3.Seviye Bulmaca
![puzz](ss/puzz3.png)
<br>

###### Örnek Tahminli Bulmaca
![puzz](ss/puzz4.png)
<br>

## Kaynaklar
1. [Access a child from the parent or other gameObject](https://answers.unity.com/questions/63317/access-a-child-from-the-parent-or-other-gameobject.html)
2. [Transform.childCount](https://docs.unity3d.com/ScriptReference/Transform-childCount.html)
3. [Unity Mobile From Scratch: TouchPhases and Touch Count](https://www.youtube.com/watch?v=ay9bbWJQ01w)
4. [Disable UI button](https://answers.unity.com/questions/1225741/disable-ui-button.html)
5. [Best way to communicate between scripts](https://answers.unity.com/questions/1450557/best-way-to-communicate-between-scripts.html)
6. [Selectable.interactable](https://docs.unity3d.com/540/Documentation/ScriptReference/UI.Selectable-interactable.html)
7. [TextMesh Pro - Font Asset Creation](https://www.youtube.com/watch?v=qzJNIGCFFtY&feature=youtu.be)
8. [GraphicRaycaster.Raycast](https://docs.unity3d.com/2017.3/Documentation/ScriptReference/UI.GraphicRaycaster.Raycast.html)
9. [Raycast in unity2D using mouse Position](https://sushanta1991.blogspot.com/2015/01/raycast-in-unity2d-using-mouse-position.html)
10. [TOUCH CONTROLS in Unity!](https://www.youtube.com/watch?v=bp2PiFC9sSs)
