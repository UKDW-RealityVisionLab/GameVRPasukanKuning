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
    private AIBehaviour ai;

    public string[] GetExplanation(AIBehaviour ai)
    {
        if(ai.Type == NPCType.GuidanceSeller)
        {
            explanation = new string[]{
            "Jika punya barang daur ulang bisa dijual ke saya",
            "Setiap kamu menjual barang daur ulang itu membantu lingkungan.",
            "Saya tidak menerima barang yang tidak di daur ulang",
            "Untuk harga bisa dibicarakan",
            "Jika kamu ingin menjual barang bisa di saya."};
        }
        if (ai.Type == NPCType.GuidanceCrafter)
        {
            explanation = new string[]{
            "Aku bisa ubah sampah plastikmu jadi kursi!",
            "Bawa aku bahan bekas, nanti kubuat jadi barang berguna.",
            "Aku bisa ubah botol plastikmu jadi vas bunga.",
            "Sampah logam bisa jadi karya seni.",
            "Karton bekas bisa jadi rak buku.",
            "Aku membuat kerajinan dari barang buangan.",
            "Kita bisa ciptakan barang berguna dari sampah.",
            "Plastik keras bisa diubah jadi mainan.",
            "Kaleng minuman bisa jadi pot bunga unik.",
            "Ban bekas? Bisa jadi kursi keren!",
            "Aku percaya tidak ada yang benar-benar 'sampah'.",
            "Seni terbaik kadang berasal dari barang tak terpakai."};
        }
        if (ai.Type == NPCType.GuidanceInfoHelper)
        {
            explanation = new string[]{
            "Tempat pembuangan sampah ada di barat desa.",
            "Ingat, sampah organik dan anorganik harus dipisah!",
            "Sampah organik bisa dijadikan kompos.",
            "Pisahkan sampah berdasarkan jenis: organik, anorganik, B3.",
            "Tempat pembuangan akhir ada di ujung jalan selatan.",
            "Di dekat sekolah ada tempat pengumpulan plastik.",
            "Pemerintah desa sedang kampanye anti sampah plastik.",
            "Bulan ini ada lomba daur ulang antar-warga!",
            "Tong warna hijau untuk organik, kuning untuk anorganik.",
            "Jangan buang sampah elektronik sembarangan.",
            "Bank sampah buka setiap hari Minggu pagi.",
            "Cek papan informasi untuk jadwal pengangkutan sampah."};
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
            ""};
        }
        if (ai.Type == NPCType.GuidanceCrafter)
        {
            question = new string[]{
            "Kamu punya botol bekas atau kardus?",
            "Mau kubuatkan komposter dari kayu bekas?",
            "Kamu punya barang bekas yang bisa kubuatkan sesuatu?",
            "Mau kubuatkan kerajinan dari botolmu?",
            "Sudah pernah lihat karya dari sampah daur ulang?",
            "Punya kardus bekas? Aku bisa buat jadi rak.",
            "Mau komposter dari kayu bekas?",
            "Butuh tempat alat tulis dari kaleng?",
            "Ada bahan plastik yang tak kamu pakai?",
            "Mau lihat proses pembuatan barang daur ulang?",
            "Punya ide barang yang ingin kamu buat?",
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

        return randomChat;
    }
}
