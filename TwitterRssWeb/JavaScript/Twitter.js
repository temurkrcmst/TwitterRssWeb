var uri = '/TwitterRssWeb/api/twitter/gettwitter'

var Service = (function (m) {
   
    m.Twitter2 = {
        Get: function (cb) {//listeleme isteği
            
            $.ajax({
                type: 'GET',
                //data: JSON.stringify(Getobj),
                dataType: "json",
                contentType: "application/json",
                url: '/api/twitter/gettwitter',
                success: function (data) {
                    cb(data)
                }

            });

        },
        Update: function (params,cb) {//gücelleme isteği
            $.ajax({
                type: 'POST',// istek türü
                data: JSON.stringify(params),//Gönderilen objelerin backend kısmına aktarılması..
                dataType: "json",//Format bilgisi
                contentType: "application/json",
                url: '/api/twitter/UptadeTwitter',//isteğin izleyeceği yol
                success: function (data) {
                    cb(data)
            }
            });
        },

        }
    return m;
}(Service || {}))


//Service.Twitter2.Get(function (data) {
//    console.log(data);
//    $(".infoArea").html("").append(data); //jquery fonksiyonu  . varsa class // #id

//});

$(document).ready(function () {
   
    Service.Twitter2.Get(function (data) {//listeleme isteğinin yolladığı funtion.
        var items = '';
        var rows = " <div>" +
            "<div role='row'>" 

             "</div>";

        $.each(data, function (i, item) {

            var date = new Date(item.pubdate).toLocaleDateString();// Veritabanından getirilen tarihi "Tarih ve Zamanı" İki farklı değere böldük böylece saat kısmını daha belirgin göstermek için.  
            var time = new Date(item.pubdate).toLocaleTimeString();
            var rows = '<div role="row" class="odd">' +
                ' <div class="sorting_1">' + date +'-'+'<strong>'+ time +'</strong>' +'   -   ' + item.title + '   ' +
                '<input id="' + item.guid + '" type="checkbox" class="CheckboxClass" name="twitcheck" id="exampleCheck1">' + '</div>' +
                '<hr>'+
                ' </div>';

            
            
        $('#dataTable tbody').append(rows);


            if (item.checkeded == "1") {//Checkbox'ların işaretlendiği zaman gönderilecek bilgiler ve oluşacak durum...
                $("#" + item.guid + "").attr('checked', 'checked');
                document.getElementById(item.guid).disabled = true;

            } else {
                
            }
        });

     
      
        $('.CheckboxClass').click(function () {//checkbox 1/0 bilgisi
            
            var obj = {
                guid: "",
                checkeded: 0,
               

            };
            var answer = confirm("Değişiklik kaydedilsin mi?")//Uyarı Ekranı verilip onayladıktan sonraki checkbox kısmını pasif hale getirilme aşaması...
            if (answer) {
                if ($(this).is(':checked')) {

                    console.log($(this));
                    obj.guid = this.id;
                    obj.checkeded = 1;
                    document.getElementById(obj.guid).disabled = true;

                }
            }
            else {
                //some code console.log($(this));
                
                obj.guid = this.id;
                obj.checkeded = 0;
                document.getElementById(this.id).checked = false;
            }
            Service.Twitter2.Update(obj, function (data) {



            })
        });
    });
    //$(".CheckboxClass").click(function () {
    //    var x = document.getElementsByClassName("CheckboxClass").checked;

    //})
   
    //Service.Twitter2.Update(function (data) {

       



    //});
    
  
});