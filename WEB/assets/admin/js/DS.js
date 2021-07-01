$(document).ready(function () {
    GetDuLieu();
    GetDuLieuAllNam();
    gettatcatnguoithue();
    getnguoidangthue();
    getphongdangthue();
    getphongtrong();
})

function GetDuLieu(value) {
    Array.prototype.insert = function (index, item) {
        this.splice(index, 0, item);
    };
    if (value == undefined) {
        var d = new Date();
        value = d.getFullYear();
    }
    var t = [];
    var z = [];
    var barColors = ["orange", "orange", "orange", "orange", "orange", "orange", "orange", "orange", "orange", "orange", "orange", "orange"];
    $.ajax({
        url: '/Dashboard/DsDoanhThuTungThang',
        type: 'get',
        data: {
            nam: value
        },
        success: function (data) {
            if (data.code = 200) {
                $.each(data.ds, function (k, v) {
                    t.insert(0, "Tháng " + v.Thang)
                    z.insert(0, v.DOANTHU)
                });
                t.reverse();
                z.reverse();
                new Chart("myChart", {
                    type: "bar",
                    data: {
                        labels: t,
                        datasets: [{
                            backgroundColor: barColors,
                            data: z
                        }]
                    },
                    options: {
                        legend: { display: false },
                        title: {
                            display: true,
                            text: "Thống kê doanh thu trong năm"
                        }
                    }
                });
            }
        }

    });

}
function GetDuLieuAllNam() {
    Array.prototype.insert = function (index, item) {
        this.splice(index, 0, item);
    };
    var xValues = ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"];
    //var yValues = [55, 49, 44, 24, 35, 55, 35, 49, 44, 24, 35, 100];
    var yValues1 = [55, 49, 44, 24, 35, 55, 35, 49, 44, 24, 35, 100];
    var t = [];
    var z = [];
    var barColors = ["orange", "orange", "orange", "orange", "orange", "orange", "orange", "orange", "orange", "orange", "orange", "orange"];
    $.ajax({
        url: '/Dashboard/DsDoanhThuTungNam',
        type: 'get',
        success: function (data) {
            if (data.code = 200) {
                $.each(data.ds, function (k, v) {
                    t.insert(0, v.Thang)
                    z.insert(0, v.DOANTHU)
                });
                t.reverse();
                z.reverse();
                new Chart("myChart1", {
                    type: "line",
                    data: {
                        labels: t,
                        datasets: [{
                            borderColor: 'rgb(75, 192, 192)',
                            data: z
                        }]
                    },
                    options: {
                        legend: { display: false },
                        title: {
                            display: true,
                            text: "Thống kê doanh thu từng năm"
                        }
                    }
                });

            }
        }

    });

}
function onChangeFunction(value) {
    GetDuLieu(value);
}
function gettatcatnguoithue() {
    $.ajax({
        url: '/Dashboard/TongCongKhachThue',
        type: 'get',
        success: function (data) {
            if (data.code = 200) {
                document.getElementById("GetNguoiThue").innerHTML = data.dskt;

            } else if (data.code = 500) {
                alert("Lấy thất bại");
            }
        }
    });
}
function getnguoidangthue() {
    $.ajax({
        url: '/Dashboard/TongCongKhachDangThue',
        type: 'get',
        success: function (data) {
            if (data.code = 200) {
                document.getElementById("GetDangThue").innerHTML = data.TC;

            } else if (data.code = 500) {
                alert("Lấy thất bại");
            }
        }
    });
}
function getphongdangthue() {
    $.ajax({
        url: 'Dashboard/SoPhongTrongDangThue',
        type: 'get',
        success: function (data) {
            if (data.code = 200) {
                document.getElementById("phongdangthue").innerHTML = data.TC;
            }
        }
    });
}
function getphongtrong() {
    $.ajax({
        url: 'Dashboard/SoPhongTrong',
        type: 'get',
        success: function (data) {
            if (data.code = 200) {
                document.getElementById("phongtrong").innerHTML = data.TC;
            }
        }
    });
}