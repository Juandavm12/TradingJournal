function saveAsExcel(fileName, byteContent) {
    var link = document.createElement('a');
    link.download = fileName;
    console.log(fileName);
    link.href = 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + byteContent;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}