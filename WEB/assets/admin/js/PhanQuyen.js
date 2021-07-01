history.forward();
$(document).ready(function () {
    LoadTaikhoan();
})
function LoadTaikhoan() {
        
    $.ajax({
        url: '/Dashboard/LoadPhanQuyen',
        type: 'get',
        success: function (data) {               
            if (data.code = 200)
            {
                $.each(data.pq, function (k, v) {
                    let ma = v.MANV;
                    let tr = '<tr id="'+v.MANV+'">'
                    tr+='<td>'+v.TENNV+'</td>';
                    tr+='<td>'+v.USERNAME+'</td>';
                    tr+='<td>';
                    tr+='<select  aria-label="Default select example" name="doipq'+ma+'" id="mySelect'+ma+'"></select>';
                    tr+='</td>';
                    tr+='</tr>';
                    $('#Dstaikhoan').append(tr);
                    let quyen = v.QUYEN;               
                    $.ajax({
                        url: '/Dashboard/LoadTenQuyen',
                        type: 'get',
                        success: function (data) {
                            if (data.code = 200)
                            {
                                var x = document.getElementById("mySelect"+ma+"");
                                $.each(data.dsq, function (k, v) {
                                    var option = document.createElement("option");   
                                    option.text = v.TEN;
                                    option.value = v.ID;
                                    x.add(option);
                                    document.getElementById("mySelect"+ma+"").value = quyen;
                                });  
                                $(document.body).on('change', 'select[name^="doipq'+ma+'"]', function(){
                                    let nv = $(this).closest('tr').attr('id');
                                    let doi = document.getElementById("mySelect"+ma+"").value;
                                    $.ajax({
                                        url:'/Dashboard/DoiQuyen',
                                        type:'post',
                                        data:{
                                            manv:nv,
                                            mapq:doi
                                        },
                                        success: function(data){
                                            if(data.code=200)
                                            {
                                                alert(data.msg);
                                            }else if(data.code=500)
                                            {
                                                alert(data.msg);
                                            }
                                        }
                                    });
                                });
                            }
                                
                        }
                    });
                });
                  
            }
               
        }
    });       
}

