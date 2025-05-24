using Pico.Platform.Models;
using System;
using System.Collections.Generic;


[Serializable]
public class ListContext
{
    private string[] explanation;
    private string[] question;
    private string[] offer;
    private string[] answer;
    private string[] randomChat;
    private string[] everyDayChat;
    private string[] angryChat;
    private string[] afterAngryChat;
    private string[] introduction;
    private AIBehaviour ai;

    public string[] GetIntroduction(AIBehaviour ai)
    {

        if (ai.Type == NPCType.GuidanceSeller)
        {
            introduction = new string[]
            {
            "Halo! Aku yang mengurus jual beli barang daur ulang di sini. Kalau kamu bawa sampah bernilai, langsung saja ke aku ya. Selamat datang di game ini!"
            };
        }
        if (ai.Type == NPCType.GuidanceCrafter)
        {
            introduction = new string[]
            {
            "Hai! Aku suka bikin hal-hal keren dari barang bekas. Kalau kamu punya sesuatu yang bisa diubah jadi kerajinan, bawa ke aku. Senang bisa bertemu di game ini!"
            };
        }
        if (ai.Type == NPCType.GuidanceInfoHelper)
        {
            introduction = new string[]
            {
            "Hey! Aku di sini buat bantu kamu ngerti cara main dan hal-hal penting dalam game ini. Kalau butuh petunjuk atau arah, tanya aja ya. Selamat datang dan semoga seru mainnya!"
            };
        }

        return introduction;
    }


    public string[] GetExplanation(AIBehaviour ai, string destination)
    {
        if(ai.Type == NPCType.GuidanceSeller)
        {
            explanation = new string[]{
            "Jika punya barang daur ulang bisa dijual ke saya",
            "Setiap kamu menjual barang daur ulang itu membantu lingkungan.",
            "Saya tidak menerima barang yang tidak di daur ulang",
            "Untuk harga bisa dibicarakan",
            "Jumlah Barang yang kamu tawarkan bisa saya naikkan harganya"};
        }
        if (ai.Type == NPCType.GuidanceCrafter)
        {
            explanation = new string[]{
            "Saya bisa buatkan barang, tapi transmute dulu sampahmu di tong sampah",
            "Bawakan aku bahan bekas, nanti kubuat jadi barang berguna.",
            "2 logam dan 2 plastik bisa jadi karya seni seperti patung anjing ini.",
            "lalu 2 logam dan 2 kaca bisa jadi cermin seperti ini.",
            "ada juga 2 plastik dan 1 logam bisa jadi tas cantik.",
            "setelah itu 1 plastik dan 1 logam bisa jadi kalung perhiasan.",
            "1 logam saja juga bisa berubah menjadi kaleng baru",
            "Aku bisa ubah sampah plastikmu jadi botol yang bisa dijual."};
        }
        if (ai.Type == NPCType.GuidanceInfoHelper)
        {
            switch (destination)
            {
                case "TrashBin":
                    return explanation = new string[] {
                "Disini tempat sampahnya.",
                "Ingat, sampah organik dan anorganik harus dipisah!",
                "Sampah organik bisa dijadikan kompos.",
                "Pisahkan sampah berdasarkan jenis: organik, anorganik, B3.",
                "Pemerintah sekarang sedang kampanye anti sampah plastik.",
                "Tong warna hijau untuk organik, kuning untuk anorganik.",
                "Jangan buang sampah elektronik sembarangan.",
                };

                case "SellPlace":
                    return explanation = new string[] {
                "Kalau kamu ingin menjual barang hasil daur ulang, ini tempatnya.",
                "Jual barang daur ulangmu di sini untuk dapat koin!",
                "Kualitas barangmu menentukan harga jualnya!",
                "kamu juga bisa menawar harga disini"
                };

                case "CraftPlace":
                    return explanation = new string[] {
                "Di sini tempat Crafter membuat kerajinan dari sampah.",
                "Temui Crafter untuk ubah sampah jadi barang berguna.",
                "Kreativitasmu akan dibantu di tempat ini.",
                "Crafter bisa membuat sesuatu dari barang bekasmu."
                };
            }
        }

        return explanation;
    }
    public string[] GetQuestion(AIBehaviour ai)
    {
        if (ai.Type == NPCType.GuidanceSeller)
        {
            question = new string[]{
            "Ada yang bisa Saya bantu?",
            "Anda memiliki berapa barang?",
            "Apa yang ingin Anda tanyakan?",
            "Sedang ingin berbisnis dengan Saya?",
            "Butuh Bantuan apa ya kak?",
            "Anda ingin Berurusan dengan saya?",
            "Keperluannya apa ya kak?"};
        }
        if (ai.Type == NPCType.GuidanceCrafter)
        {
            question = new string[]{
            "Kamu punya botol bekas atau kardus?",
            "Kamu punya barang bekas yang bisa kubuatkan sesuatu?",
            "Mau kubuatkan kerajinan dari botolmu?",
            "Sudah pernah lihat karya dari sampah daur ulang?",
            "Butuh tempat alat tulis dari kaleng?",
            "Ada bahan plastik yang tak kamu pakai?",
            "Mau tukar sampahmu dengan karya tanganku?"};
        }
        if (ai.Type == NPCType.GuidanceInfoHelper)
        {
            question = new string[]{
            "Sudah tahu cara memilah sampah?",
            "Butuh bantuan cari lokasi daur ulang?",
            "Tahu ke mana sampahmu pergi setelah dibuang?",
            "Sudah ikut program pemilahan sampah belum?",
            "Tahu beda sampah B3 dan anorganik biasa?",
            "Kamu pernah ke tempat daur ulang kota?",
            "Mau bantu kampanye kebersihan minggu ini?",
            "Kamu tahu siapa pengelola bank sampah di sini?",
            "Pernah ikut kerja bakti lingkungan?",
            "Tahu cara mengolah sampah dapur sendiri?",
            "Butuh peta lokasi tong sampah terdekat?",
            "Kamu ingin belajar cara membuat kompos?"};
        }

        return question;
    }

    public string[] GetEmotion(AIBehaviour ai)
    {
        if (ai.Type == NPCType.BystanderChild)
        {
            if (ai.emotion.ToLower() == "happy")
            {
                everyDayChat = new string[] {
                "Tugu itu kayak roket mau terbang ke langit!",
                "Aku suka lari-larian keliling Tugu sampai capek!",
                "Kadang aku pura-pura jadi turis juga, hehe.",
                "Main di sini rame banget, seruuu!",
                "Aku nemu batu yang lucu banget di bawah bangku!"
                };
            }
            if (ai.emotion.ToLower() == "sad")
            {
                everyDayChat = new string[] {
                "Temenku nggak jadi datang... jadi aku main sendiri.",
                "Tadi balonku hilang kena angin, sedih deh.",
                "Aku jatuh waktu lari tadi, sakit banget.",
                "Mainan ku rusak pas di jalan tadi.",
                "Aku dimarahin mama soalnya baju kotor."
                };
            }
            if (ai.emotion.ToLower() == "wondering")
            {
                everyDayChat = new string[] {
                "Kenapa ya Tugu itu bisa nyala malam-malam?",
                "Kalau aku naik ke puncak Tugu, bisa kelihatan rumahku nggak ya?",
                "Itu burung apa ya yang sering lewat atas situ?",
                "Apa di dalam Tugu ada tangga rahasianya?",
                "Kok bisa ya Tugu berdiri tegak kayak gitu?"
                };
            }
        }
        if (ai.Type == NPCType.BystanderTourist)
        {
            if (ai.emotion.ToLower() == "happy")
            {
                everyDayChat = new string[] {
                "Akhirnya bisa foto di Tugu Jogja juga!",
                "Suasananya menyenangkan banget di sini.",
                "Suka banget sama suasana malam di sekitar Tugu!",
                "Banyak spot foto yang keren di sini!",
                "Wah, banyak jajanan enak dekat sini."
                };
            }
            if (ai.emotion.ToLower() == "sad")
            {
                everyDayChat = new string[] {
                "Sayang banget, jalannya agak kotor ya...",
                "Aku kecopetan... padahal baru sebentar di sini.",
                "Tadi kameraku jatuh, lensanya retak.",
                "Gagal dapet sunset karena mendung.",
                "Nggak sempat foto karena terlalu rame."
                };
            }
            if (ai.emotion.ToLower() == "wondering")
            {
                everyDayChat = new string[] {
                "Siapa ya yang bangun Tugu ini pertama kali?",
                "Apa Tugu ini ada sejarahnya yang misterius?",
                "Kenapa Tugu jadi simbol Jogja ya?",
                "Di dalam Tugu ini ada ruang kosong nggak sih?",
                "Tugu ini bisa roboh kalau gempa besar ya?"
                };
            }
        }
        if (ai.Type == NPCType.BystanderWoman)
        {
            if (ai.emotion.ToLower() == "happy")
            {
                everyDayChat = new string[] {
                "Akhirnya bisa duduk santai sambil lihat Tugu.",
                "Tadi ada anak kecil lucu banget main di sini.",
                "Udara sore hari di sini enak banget.",
                "Lihat Tugu sore-sore gini bikin tenang.",
                "Kopi di warung dekat sini enak juga ya."
                };
            }
            if (ai.emotion.ToLower() == "sad")
            {
                everyDayChat = new string[] {
                "Tiap sore kok makin banyak sampah di sini.",
                "Tadi lihat kucing luka karena kena pecahan botol.",
                "Susah ya nyuruh orang buang sampah benar.",
                "Pemandangan bagus, tapi kotor bikin sedih.",
                "Kadang rasanya capek ngeluh terus soal kebersihan."
                };
            }
            if (ai.emotion.ToLower() == "wondering")
            {
                everyDayChat = new string[] {
                "Apa orang-orang bakal berubah kalau Tugu roboh karena sampah?",
                "Kenapa susah banget ya buang sampah di tempatnya?",
                "Kalau aku bawa kantong sampah sendiri, bakal berguna nggak ya?",
                "Tiap hari bersihinnya siapa ya?",
                "Apa ada CCTV di sini buat ngawasin pembuang sampah?"
                };
            }
        }
        if (ai.Type == NPCType.CulpritMale)
        {
            if (ai.emotion.ToLower() == "happy")
            {
                everyDayChat = new string[] {
                "Ngopi sore di sini emang paling pas!",
                "Asik nih, adem dan sepi.",
                "Rokok satu, pemandangan satu. Komplet!",
                "Tugu ini spot paling enak buat santai.",
                "Habis kerja, nongkrong di sini tuh nikmat."
                };
            }
            if (ai.emotion.ToLower() == "sad")
            {
                everyDayChat = new string[] {
                "Ditegur lagi gara-gara sampah... males ah.",
                "Tadi diomelin orang yang nggak kenal, bete.",
                "Motor mogok pas jalan ke sini. Sial!",
                "Lagi ribut di rumah, makanya mampir sini.",
                "Kerjaan kacau, cuma Tugu yang bisa nenangin."
                };
            }
            if (ai.emotion.ToLower() == "wondering")
            {
                everyDayChat = new string[] {
                "Tugu ini kayaknya nyimpan cerita lama deh.",
                "Kalau tiba-tiba Tugu ini bisa ngomong, dia bakal marah ya?",
                "Apa bener Tugu ini pernah digeser waktu Belanda dulu?",
                "Kalau ada yang bersihin tiap malam, kok masih kotor ya?",
                "Apa aku harus buang sampah ke tong juga?"
                };
            }
        }
        if (ai.Type == NPCType.CulpritChild)
        {
            if (ai.emotion.ToLower() == "happy")
            {
                everyDayChat = new string[] {
                "Yay! Bungkus permenku bisa diterbangin angin!",
                "Aku tadi bikin pesawat dari kertas bekas!",
                "Main di sini lebih seru dari taman belakang rumah!",
                "Ada kucing ikutan lari-larian sama aku!",
                "Aku nemu tutup botol warna emas, keren banget!"
                };
            }
            if (ai.emotion.ToLower() == "sad")
            {
                everyDayChat = new string[] {
                "Tadi dimarahin karena buang sampah sembarangan...",
                "Kakiku luka kena pecahan beling...",
                "Mainanku nyemplung got tadi...",
                "Ada yang rebut tempat dudukku di bangku.",
                "Aku disuruh pulang, padahal belum puas main."
                };
            }
            if (ai.emotion.ToLower() == "wondering")
            {
                everyDayChat = new string[] {
                "Kalau aku buang sampah ke tanah, jadi pohon nggak ya?",
                "Kalau robot datang, dia bisa makan sampah nggak?",
                "Apa Tugu ini bisa gerak kayak di film?",
                "Kalo aku buang bungkus, ada yang liatin nggak ya?",
                "Di balik Tugu ada harta karun nggak ya?"
                };
            }
        }
        if (ai.Type == NPCType.CulpritOldman)
        {
            if (ai.emotion.ToLower() == "happy")
            {
                everyDayChat = new string[] {
                "Ngopi sambil duduk lihat Tugu, nikmat dunia.",
                "Pagi-pagi sepi, enak banget buat duduk.",
                "Saya dulu sering bawa anak main ke sini.",
                "Lihat burung pagi-pagi tuh bikin adem pikiran.",
                "Ada yang sapa saya tadi, katanya masih inget saya jaga ronda."
                };
            }
            if (ai.emotion.ToLower() == "sad")
            {
                everyDayChat = new string[] {
                "Sekarang beda, tempat ini makin ramai dan kotor.",
                "Kadang saya duduk sendiri, nggak ada teman ngobrol.",
                "Pinggang mulai nyeri, duduk pun harus pelan-pelan.",
                "Orang sekarang jarang sapa, sibuk sama gadget.",
                "Saya dimarahin cuma karena buang puntung..."
                };
            }
            if (ai.emotion.ToLower() == "wondering")
            {
                everyDayChat = new string[] {
                "Zaman dulu, Tugu ini ada cerita mistisnya lho...",
                "Apa ya yang bakal terjadi kalau Tugu ini diganti?",
                "Dulu Tugu segini juga ya tingginya?",
                "Ada petugas yang jaga kebersihan tiap malam ya?",
                "Kalau saya duduk sini tiap hari, ada yang inget saya nggak ya?"
                };
            }
        }
        return everyDayChat;
    }

    public string[] GetRandomChat(AIBehaviour ai)
    {
        if (ai.Type == NPCType.GuidanceSeller)
        {
            randomChat = new string[]{
            "Hmmm... barang ini harusnya dipajang di rak depan.",
            "Sudah waktunya buang sampah ke tempat hijau.",
            "Aku butuh lebih banyak kantong kompos untuk stok minggu ini.",
            "Kalau alat pemadat ini rusak, harus pesan baru lagi.",
            "Setiap hari aku bersihkan area jualanku biar nyaman.",
            "Kadang orang lupa, sampah organik itu bisa jadi pupuk.",
            "Aku senang kalau lihat anak-anak mulai belajar memilah sampah.",
            "Harus rajin menata ulang rak supaya barang terlihat menarik.",
            "Baru saja ada yang beli alat cuci sampah otomatis. Laris juga!",
            "Semoga hari ini banyak yang sadar pentingnya daur ulang.",
            "Hmm... harus cek stok tong sampah pintar lagi.",
            "Waktunya naruh papan promo baru, biar makin menarik."};
        }
        if (ai.Type == NPCType.GuidanceCrafter)
        {
            randomChat = new string[]{
            "Botol bekas ini masih bisa jadi tempat pensil.",
            "Wah, lem kayu-ku hampir habis. Harus beli lagi.",
            "Aku suka bikin pot bunga dari kaleng, bentuknya unik.",
            "Rak buku dari kardus bekas ini cukup kuat, loh!",
            "Harus hati-hati saat motong plastik tebal.",
            "Hari ini aku coba desain baru dari tutup botol.",
            "Bikin sesuatu dari barang tak terpakai itu memuaskan.",
            "Minggu depan aku mau pameran kerajinan daur ulang.",
            "Barang bekas tak selalu berarti tak bernilai.",
            "Kalau ada ban bekas lagi, aku bisa bikin ayunan.",
            "Aduh, benang daur ulangku kusut lagi.",
            "Lemari dari peti bekas? Bisa juga tuh!"};
        }
        if (ai.Type == NPCType.GuidanceInfoHelper)
        {
            randomChat = new string[]{
              "Baru tadi pagi aku lihat ada yang buang sampah sembarangan. Sayang banget.",
              "Aku baru tempel pamflet baru soal jadwal daur ulang!",
              "Kadang tong sampah penuh sebelum truk datang.",
              "Warga RT 04 minggu ini paling rajin pilah sampah.",
              "Aku suka bantu anak-anak paham soal jenis sampah.",
              "Bank sampah minggu lalu penuh banget, luar biasa!",
              "Sore ini ada penyuluhan soal limbah B3 di balai desa.",
              "Aku ingin buat peta interaktif lokasi tong sampah.",
              "Kampanye anti plastik sekali pakai masih terus berjalan.",
              "Kadang warga bingung antara sampah anorganik dan B3.",
              "Aku selalu catat siapa saja yang aktif ikut bersih-bersih.",
              "Tong warna biru itu masih sering disalahgunakan."};
        }
        if (ai.Type == NPCType.CulpritMale)
        {
            randomChat = new string[]{
                "Asap rokok emang paling enak dinikmati pas sore begini.",
                "Buang bungkus rokok? Ah, kecil gitu doang dibikin ribet.",
                "Tiap lewat sini, selalu beli kopi dan duduk lama-lama aja.",
                "Tugu ini emang cocoknya buat santai, bukan mikirin sampah.",
                "Kemarin ada yang negur soal sampah, tapi saya senyum aja.",
                "Bakar rokok, lihat pemandangan... lengkap sudah.",
                "Ngapain repot nyari tong sampah? Di sini juga bisa.",
                "Dulu mah nggak seribet sekarang soal buang bungkus ini itu.",
                "Saya udah langganan duduk di sini tiap sore, nggak ada yang ganggu.",
                "Yang penting nggak ganggu orang, urusan sampah mah nanti juga dibersihin petugas.",
                "Tong sampah kadang jauh banget, makanya ya udah lempar aja.",
                "Sini adem, enak buat ngopi sambil nikmatin angin."};
        }

        if (ai.Type == NPCType.CulpritChild)
        {
            randomChat = new string[]{
              "Aku lempar bungkus permen ke got, terus airnya ciprat! Hehe.",
              "Aku main balapan sama daun kering di jalan!",
              "Tadi bungkus esku diterbangin angin... ya udah, pergi deh!",
              "Aku lihat kucing loncat dari tong sampah! Lucu banget!",
              "Kalau aku lempar bungkus ke udara, dia bisa terbang kayak burung!",
              "Main di sini lebih seru daripada disuruh nyapu rumah.",
              "Aku suka ngumpetin sampah bungkus di balik bangku. Rahasia!",
              "Kak, aku barusan bikin pesawat dari kertas... terus aku lempar ke selokan!",
              "Aku nggak tau harus buang di mana... jadi aku masukin ke pot bunga.",
              "Aku tadi main robot-robotan dari botol bekas, terus bosennya dibuang aja.",
              "Aku lari-lari terus jatuhin bungkus jajan... tapi nggak mau poool!",
              "Kucingnya suka ikut aku main, kadang malah gigitin bungkus jajan."};
        }
        if (ai.Type == NPCType.CulpritOldman)
        {
            randomChat = new string[]{
              "Zaman saya dulu, nggak ada yang ribut soal sampah begini.",
              "Asal nggak di tengah jalan, buang sampah mah sah-sah aja.",
              "Saya udah tua, masa masih disuruh pilah sampah segala.",
              "Di umur segini, yang penting duduk nyaman, ngerokok santai.",
              "Kalau tiap kali rokok harus cari tong sampah, bisa gempor saya.",
              "Ngopi pagi sambil lihat lalu lintas, itu baru hidup.",
              "Saya dulu juga bersihin lingkungan, sekarang gantian yang muda dong.",
              "Itu tong sampah warnanya aneh, bikin bingung aja.",
              "Bungkus permen jatuh? Ya udah lah, nanti juga hilang sendiri.",
              "Duduk di sini tiap pagi udah jadi kebiasaan sejak lama.",
              "Kata siapa buang sampah sembarangan bikin bumi marah? Saya aman-aman aja.",
              "Anak muda sekarang cerewet banget soal plastik, dulu mah cuek aja."};
        }
        if (ai.Type == NPCType.BystanderChild)
        {
            randomChat = new string[]{
              "Tugu itu kayak roket mau terbang ke langit!",
              "Aku sering main petak umpet sama temenku di sekitar sini!",
              "Aku pernah lihat balon nyangkut di atas Tugu!",
              "Kalau malam, Tugu nyala kayak lampu ajaib!",
              "Kadang aku pura-pura jadi penjaga Tugu, berdiri tegap gitu!",
              "Tadi aku lihat badut lewat sini, lucu banget!",
              "Aku pengen naik ke puncak Tugu, tapi kata orang tua dilarang.",
              "Aku suka lari-larian keliling Tugu sampai capek!",
              "Tadi aku nemu batu lucu di deket pagar Tugu!",
              "Di sini rame terus, tapi seru!",
              "Aku suka duduk sambil liat motor-motor lewat.",
              "Kadang aku pura-pura jadi turis juga, hehe."};
        }

        if (ai.Type == NPCType.BystanderTourist)
        {
            randomChat = new string[]{
              "Akhirnya bisa foto di Tugu Jogja juga!",
              "Rame ya di sekitar sini, cocok buat kulineran malam.",
              "Tadi sempat liat ada orang buang sampah... sayang ya.",
              "Tempat ini ikonik banget, wajib mampir!",
              "Banyak yang jual kopi dekat sini, suasananya syahdu banget.",
              "Dari hotel deket sini keliatan Tugu, keren pas malam.",
              "Lumayan kotor ya pinggiran jalannya, tapi ya udah lah...",
              "Abis foto-foto, mau lanjut cari bakpia nih!",
              "Tugu Jogja ini kayak landmark wajib, kata temen-temen.",
              "Asal nggak terlalu rame, di sini enak buat nongkrong sore.",
              "Semoga tetap bersih ya tempat ikonik begini.",
              "Banyak yang naik becak muter-muter Tugu, unik juga!"};
        }

        if (ai.Type == NPCType.BystanderWoman)
        {
            randomChat = new string[]{
              "Kalau semua orang buang sampah seenaknya, Tugu ini jadi tempat sampah raksasa.",
              "Foto-foto sih boleh, tapi jangan buang plastik sembarangan dong.",
              "Katanya ikon kota, tapi pinggirannya penuh puntung rokok.",
              "Duduk manis sambil lihat Tugu, sambil hitung berapa orang buang sampah sembunyi-sembunyi.",
              "Kadang mikir, yang datang ke sini lebih sayang feed Instagram daripada kotanya.",
              "Sudah cantik Tugu-nya, tolong dong dijaga juga lingkungannya.",
              "Saya liatin aja, siapa tahu ada yang malu terus ambil sampahnya sendiri.",
              "Setiap sore saya lihat sampah numpuk, terus bersih lagi, terus numpuk lagi. Capek deh.",
              "Tiap ada acara, pasti ada yang ninggalin botol plastik. Nggak ada kapoknya.",
              "Mau selfie? Silakan. Mau buang sampah? Silakan balik kanan dulu.",
              "Tong sampah deket situ padahal ada, tapi kok masih buang sembarangan ya?",
              "Saya sih udah sering bilang... tapi ya gitu, masuk kuping kiri keluar bungkusan."};
        }

        return randomChat;
    }
    public string[] GetAngryChat(AIBehaviour ai)
    {
        if (ai.Type == NPCType.GuidanceSeller)
        {
            angryChat = new string[] {
            "Kalau cuma mau pencet tombol, jangan ganggu saya.",
            "Saya sedang sibuk, tolong jangan iseng.",
            "Jangan pencet terus! Saya juga bisa marah!",
            "Kalau tidak niat bicara, jangan ajak ngobrol."
        };
        }
        if (ai.Type == NPCType.GuidanceCrafter)
        {
            angryChat = new string[] {
            "Aku nggak punya waktu buat main-main tombol.",
            "Tolong seriuslah! Ini bukan permainan.",
            "Kalau nggak ada yang penting, jangan ganggu aku.",
            "Sudah cukup pencet-pencetnya!"
        };
        }
        if (ai.Type == NPCType.GuidanceInfoHelper)
        {
            angryChat = new string[] {
            "Hei! Jangan pencet tombol sembarangan!",
            "Kalau kamu butuh bantuan, bilang saja. Jangan main-main.",
            "Aku bisa pergi kalau kamu terus begini.",
            "Seriuslah sedikit, ini soal lingkungan!"
        };
        }
        if (ai.Type == NPCType.CulpritMale)
        {
            angryChat = new string[] {
            "Eh, kamu ngapain sih ganggu-ganggu orang?!",
            "Udah lah, jangan ikut campur urusan gue.",
            "Kalau rese, gue cabut nih!",
            "Pencet terus, situ kira saya robot?"
        };
        }
        if (ai.Type == NPCType.CulpritChild)
        {
            angryChat = new string[] {
            "Huh! Aku nggak mau main lagi!",
            "Kakak jahat! Jangan pencet-pencet terus!",
            "Aku bilang BERHENTI!",
            "Aku laporin ke ibu lho!"
        };
        }
        if (ai.Type == NPCType.CulpritOldman)
        {
            angryChat = new string[] {
            "Kamu kira saya ini patung?!",
            "Jangan pencet-pencet! Tangan kamu gatel ya?",
            "Saya udah tua, jangan bikin pusing!",
            "Udah cukup ganggunya. Pergi sana!"
        };
        }
        if (ai.Type == NPCType.BystanderChild)
        {
            angryChat = new string[] {
            "Ish! Jangan pencet aku terus dong!",
            "Aku bilang berhenti! Nanti aku marah!",
            "Kakak ganggu aja, aku mau main sendiri!",
            "Aku nggak mau diajak ngobrol lagi!"
        };
        }
        if (ai.Type == NPCType.BystanderTourist)
        {
            angryChat = new string[] {
            "Tolong, jangan ganggu. Saya sedang menikmati waktu saya.",
            "Sudah cukup ya, saya bukan pemandu wisata!",
            "Saya cuma mau santai, bukan diajak ribut.",
            "Pencet terus, kamu pikir saya mesin ATM?"
        };
        }
        if (ai.Type == NPCType.BystanderWoman)
        {
            angryChat = new string[] {
            "Maaf, kamu kira saya nggak punya kesabaran?!",
            "Tombol itu bukan buat ditekan terus, tahu!",
            "Sudah ya, saya bukan badut yang bisa kamu gangguin.",
            "Kalau bosan, cari kegiatan lain, jangan saya!"
        };
        }
        return angryChat;
    }
    public string[] GetAfterAngryChat(AIBehaviour ai)
    {
        if (ai.Type == NPCType.BystanderChild)
        {
            afterAngryChat = new string[] {
            "Kamu nanya terus, aku jadi bingung!",
            "Aku mau main dulu ah, kamu gangguin terus.",
            "Ehh... jangan deket-deket terus dong!"
        };
        }
        if (ai.Type == NPCType.BystanderTourist)
        {
            afterAngryChat = new string[] {
            "Saya lagi liburan, jangan diikuti terus ya.",
            "Udah cukup, saya mau foto-foto dulu.",
            "Kamu nyari perhatian terus ya? Hehe, tapi cukup ya."
        };
        }
        if (ai.Type == NPCType.BystanderWoman)
        {
            afterAngryChat = new string[] {
            "Kamu kira aku ini pusat informasi apa gimana?",
            "Udah ya, jangan cerewet terus.",
            "Saya juga punya waktu, bukan buat ditanyain terus!"
        };
        }
        if (ai.Type == NPCType.CulpritMale)
        {
            afterAngryChat = new string[] {
            "Ngapain sih nanya-nanya mulu? Urusin aja urusanmu!",
            "Sok tau banget kamu. Pergi sana!",
            "Halah, ribet amat sih, cuma buang sampah doang!"
        };
        }
        if (ai.Type == NPCType.CulpritChild)
        {
            afterAngryChat = new string[] {
            "Ihh, kamu ganggu terus!",
            "Aku nggak mau ngomong lagi ah!",
            "Biarin! Aku buang sampah juga nggak papa kok!"
        };
        }
        if (ai.Type == NPCType.CulpritOldman)
        {
            afterAngryChat = new string[] {
            "Dari tadi kamu cerewet banget!",
            "Dulu nggak ada yang protes beginian, tahu!",
            "Udah tua-tua digangguin gitu ya..."
        };
        }
        if (ai.Type == NPCType.GuidanceSeller)
        {
            afterAngryChat = new string[] {
            "Maaf ya, saya agak emosi tadi. Kamu mau apa?",
            "Aduh, saya marah bukan karena kamu. Yuk, kita mulai lagi.",
            "Kalau kamu masih butuh sesuatu, saya tetap bantu kok."
        };
        }
        if (ai.Type == NPCType.GuidanceCrafter)
        {
            afterAngryChat = new string[] {
            "Maaf, saya kelepasan. Tapi jangan diulang ya.",
            "Saya ngerti kamu penasaran, tapi tolong sabar juga.",
            "Gak apa-apa sih, asal nggak ganggu proses saya terus ya."
        };
        }
        if (ai.Type == NPCType.GuidanceInfoHelper)
        {
            afterAngryChat = new string[] {
            "Saya di sini bantu, bukan buat dimain-mainin.",
            "Saya tetap di sini untuk bantu, tapi tolong jangan diulang ya.",
            "Saya akan jawab, tapi kalau kamu serius nanya ya.",
            "Saya bantu sebisa saya... asal kamu nggak ganggu terus."
        };
        }

        return afterAngryChat;
    }

}
