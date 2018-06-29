$(() => {     

    $("#confirm").on('click', function () {
        $.post('/Home/Confirm', { id: $(this).data('candidate-id') }, function () {
            UpdateCountsAndHideButtons();
        });     
    });  
    
    $("#refuse").on('click', function () {
        $.post('/Home/Refuse', { id: $(this).data('candidate-id') }, function () {      
            UpdateCountsAndHideButtons(); 
        });   
    });   

    function UpdateCountsAndHideButtons() {

        $.get('/home/getcounts', function (result) {  
            $("#pending-count").text(result.Pending);
            $("#confirmed-count").text(result.Confirmed);
            $("#refused-count").text(result.Refused);    
        });   
        
        $("#confirm").hide();
        $("#refuse").hide();
    }        
});