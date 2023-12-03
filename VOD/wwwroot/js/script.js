const list = [
    {
        file: './assets/video-sample/roka-video-1.mp4',
        title: 'سمینار رد هت لینوکس',
        speaker: 'مهندس معین جمالی',
        part: 'قست اول',
        comments: [
            { name: '1فاطمه زارعی', text: 'یک سوال دارم برای خرید لایسنس ردهت هم میتونیم از طریق شرکت شما اقدام کنیم؟' },
            { name: '1جهانگیر زارعی', text: 'یک سوال دارم برای خرید لایسنس ردهت هم میتونیم از طریق شرکت شما اقدام کنیم؟ یک سوال دارم برای خرید لایسنس ردهت هم میتونیم از طریق شرکت شما اقدام کنیم؟' },
            { name: '1فاطمه زارعی', text: 'یک سوال دارم برای خرید لایسنس ردهت هم میتونیم از طریق شرکت شما اقدام کنیم؟' },
            { name: '1علی زارعی', text: 'یک سوال دارم برای خرید لایسنس ردهت هم میتونیم از طریق شرکت شما اقدام کنیم؟ یک سوال دارم برای خرید لایسنس ردهت هم میتونیم از طریق شرکت شما اقدام کنیم؟' },
            { name: '1فاطمه زارعی', text: 'یک سوال دارم برای خرید لایسنس ردهت هم میتونیم از طریق شرکت شما اقدام کنیم؟' }
        ],
        like: 11,
        view: 31
    },
    {
        file: './assets/video-sample/roka-video-2.mp4',
        title: 'سمینار رد هت لینوکس',
        speaker: 'مهندس رامین مصطفی',
        part: 'قست دوم',
        comments: [
            { name: '2فاطمه زارعی', text: 'یک سوال دارم برای خرید لایسنس ردهت هم میتونیم از طریق شرکت شما اقدام کنیم؟' },
            { name: '2جهانگیر زارعی', text: 'یک سوال دارم برای خرید لایسنس ردهت هم میتونیم از طریق شرکت شما اقدام کنیم؟ یک سوال دارم برای خرید لایسنس ردهت هم میتونیم از طریق شرکت شما اقدام کنیم؟' },
            { name: '2فاطمه زارعی', text: 'یک سوال دارم برای خرید لایسنس ردهت هم میتونیم از طریق شرکت شما اقدام کنیم؟' },
            { name: '2علی زارعی', text: 'یک سوال دارم برای خرید لایسنس ردهت هم میتونیم از طریق شرکت شما اقدام کنیم؟ یک سوال دارم برای خرید لایسنس ردهت هم میتونیم از طریق شرکت شما اقدام کنیم؟' },
            { name: '2فاطمه زارعی', text: 'یک سوال دارم برای خرید لایسنس ردهت هم میتونیم از طریق شرکت شما اقدام کنیم؟' }
        ],
        like: 12,
        view: 32
    },
   
]

const appendMainVideo = (item) => {
    $(".main-video").append(`
            <div class="main-video-item">
                <div>
                    <video controls>
                        <source src=${item.file} type="video/mp4">
                    </video>
                </div>
                <div class="main-title">
                    <div class="text">
                        <span>${item.title}</span>
                        -
                        سخنران:
                        <span >${item.speaker}</span>
                        -
                        <span>${item.part}</span>
                    </div>
                    <div class="like-view">
                        <div class="share-icon"></div>

                        <div class="like-icon"></div>
                        
                        <div class="view">
                            <div class="count">${item.view}</div>
                            <div class="view-icon"></div>
                        </div>
                        
                    </div>
                </div>
            </div>`)
    item.comments.forEach(function (item) {
        $(".comments").append(`
            <div class="comment-item">
                <div class="comment-name"><span>${item.name}</span></div>
                <div class="comment-text">${item.text}</div>
                <div class="comment-line"></div>
            </div>`)
    });
}

var id = 0;

$(document).ready(function () {

    appendMainVideo(list[id]);

    list.forEach(function (item, index) {
        $(".sub-video").append(`
        <div id="${index}" class="sub-video-item">
            <div>
                <video class="video"><source src=${item.file} type="video/mp4"></video>
            </div>
            <div>
                <div class="title">${item.title} -</div>
                سخنران:
                <span class="speaker">${item.speaker}</span>
                -
                <span class="part">${item.part}</span>
            </div>
        </div>`)
        if (index === id) { $(".sub-video-item").addClass('selected'); }
    });

    $(".sub-video-item").on("click", function () {

        $(".selected").removeClass('selected');
        id = $(this).attr('id');
        $(this).addClass('selected');
        //
        $(".comment-item").remove();
        $(".main-video-item").remove();
        //
        appendMainVideo(list[id]);
    });

    $("#submit").on("click", function () {
        var inputName = $("#input-name").val();
        var inputText = $("#input-text").val();
        console.log('inputName', inputName);
        console.log('inputText', inputText);
    });

    $(".like-icon").on("click", function () {
        $(this).toggleClass('liked')

    });
})