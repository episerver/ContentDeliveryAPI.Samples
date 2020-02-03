<template>
    <div class="productImageCarousel">
        <div class="col-xs-12 col-sm-4 col--leading">
            <div id="product-carousel" class="carousel slide" data-ride="carousel">
                <div class="carousel-inner" role="listbox">
                    <div
                        class="item"
                        :class="{ active: index == 0 }"
                        :data-slide-number="index"
                        v-for="(item, index) in this.images"
                        :key="item.name"
                    >
                        <ConditionalImage :src="item" :alt="item"></ConditionalImage>
                    </div>
                </div>
            </div>
            <div class="row" id="carousel-thumbs">
                <ul class="carousel-thumbs js-carousel-thumbs">
                    <li
                        class="col-xs-3"
                        :data-slide-number="index"
                        v-for="(item, index) in this.images"
                        :key="item.name"
                    >
                        <a href="#" class="thumbnail js-thumbnail" :id="index">
                            <ConditionalImage :src="item" :alt="item"></ConditionalImage>
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script>
import ConditionalImage from '@/Scripts/components/widgets/ConditionalImage.vue';

export default {
    props: ['images'],
    components: {
        ConditionalImage
    }
};
</script>

<style lang="less" scoped>
.productImageCarousel {
    padding-top: 80px;
}

@keyframes fadein {
    from {
        opacity: 0;
    }
    to {
        opacity: 1;
    }
}

.carousel {
    position: relative;
}
.carousel-inner {
    position: relative;
    overflow: hidden;
    width: 100%;
}
.carousel-inner > .item {
    display: none;
    position: relative;
    -webkit-transition: 0.6s ease-in-out left;
    -o-transition: 0.6s ease-in-out left;
    transition: 0.6s ease-in-out left;
}
.carousel-inner > .item > img,
.carousel-inner > .item > a > img {
    line-height: 1;
}
@media all and (transform-3d), (-webkit-transform-3d) {
    .carousel-inner > .item {
        -webkit-transition: -webkit-transform 0.6s ease-in-out;
        -moz-transition: -moz-transform 0.6s ease-in-out;
        -o-transition: -o-transform 0.6s ease-in-out;
        transition: transform 0.6s ease-in-out;
        -webkit-backface-visibility: hidden;
        -moz-backface-visibility: hidden;
        backface-visibility: hidden;
        -webkit-perspective: 1000;
        -moz-perspective: 1000;
        perspective: 1000;
    }
    .carousel-inner > .item.next,
    .carousel-inner > .item.active.right {
        -webkit-transform: translate3d(100%, 0, 0);
        transform: translate3d(100%, 0, 0);
        left: 0;
    }
    .carousel-inner > .item.prev,
    .carousel-inner > .item.active.left {
        -webkit-transform: translate3d(-100%, 0, 0);
        transform: translate3d(-100%, 0, 0);
        left: 0;
    }
    .carousel-inner > .item.next.left,
    .carousel-inner > .item.prev.right,
    .carousel-inner > .item.active {
        -webkit-transform: translate3d(0, 0, 0);
        transform: translate3d(0, 0, 0);
        left: 0;
    }
}
.carousel-inner > .active,
.carousel-inner > .next,
.carousel-inner > .prev {
    display: block;
}
.carousel-inner > .active {
    left: 0;
}
.carousel-inner > .next,
.carousel-inner > .prev {
    position: absolute;
    top: 0;
    width: 100%;
}
.carousel-inner > .next {
    left: 100%;
}
.carousel-inner > .prev {
    left: -100%;
}
.carousel-inner > .next.left,
.carousel-inner > .prev.right {
    left: 0;
}
.carousel-inner > .active.left {
    left: -100%;
}
.carousel-inner > .active.right {
    left: 100%;
}
.carousel-control {
    position: absolute;
    top: 0;
    left: 0;
    bottom: 0;
    width: 15%;
    opacity: 0.5;
    filter: alpha(opacity=50);
    font-size: 20px;
    color: #fff;
    text-align: center;
    text-shadow: 0 1px 2px rgba(0, 0, 0, 0.6);
}
.carousel-control.left {
    background-image: -webkit-linear-gradient(
        left,
        rgba(0, 0, 0, 0.5) 0%,
        rgba(0, 0, 0, 0.0001) 100%
    );
    background-image: -o-linear-gradient(
        left,
        rgba(0, 0, 0, 0.5) 0%,
        rgba(0, 0, 0, 0.0001) 100%
    );
    background-image: linear-gradient(
        to right,
        rgba(0, 0, 0, 0.5) 0%,
        rgba(0, 0, 0, 0.0001) 100%
    );
    background-repeat: repeat-x;
    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#80000000', endColorstr='#00000000', GradientType=1);
}
.carousel-control.right {
    left: auto;
    right: 0;
    background-image: -webkit-linear-gradient(
        left,
        rgba(0, 0, 0, 0.0001) 0%,
        rgba(0, 0, 0, 0.5) 100%
    );
    background-image: -o-linear-gradient(
        left,
        rgba(0, 0, 0, 0.0001) 0%,
        rgba(0, 0, 0, 0.5) 100%
    );
    background-image: linear-gradient(
        to right,
        rgba(0, 0, 0, 0.0001) 0%,
        rgba(0, 0, 0, 0.5) 100%
    );
    background-repeat: repeat-x;
    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#00000000', endColorstr='#80000000', GradientType=1);
}
.carousel-control:hover,
.carousel-control:focus {
    outline: 0;
    color: #fff;
    text-decoration: none;
    opacity: 0.9;
    filter: alpha(opacity=90);
}
.carousel-control .icon-prev,
.carousel-control .icon-next,
.carousel-control .glyphicon-chevron-left,
.carousel-control .glyphicon-chevron-right {
    position: absolute;
    top: 50%;
    z-index: 5;
    display: inline-block;
}
.carousel-control .icon-prev,
.carousel-control .glyphicon-chevron-left {
    left: 50%;
    margin-left: -10px;
}
.carousel-control .icon-next,
.carousel-control .glyphicon-chevron-right {
    right: 50%;
    margin-right: -10px;
}
.carousel-control .icon-prev,
.carousel-control .icon-next {
    width: 20px;
    height: 20px;
    margin-top: -10px;
    line-height: 1;
    font-family: serif;
}
.carousel-control .icon-prev:before {
    content: "\2039";
}
.carousel-control .icon-next:before {
    content: "\203a";
}
.carousel-indicators {
    position: absolute;
    bottom: 10px;
    left: 50%;
    z-index: 15;
    width: 60%;
    margin-left: -30%;
    padding-left: 0;
    list-style: none;
    text-align: center;
}
.carousel-indicators li {
    display: inline-block;
    width: 10px;
    height: 10px;
    margin: 1px;
    text-indent: -999px;
    border: 1px solid #fff;
    border-radius: 10px;
    cursor: pointer;
    background-color: #000 \9;
    background-color: rgba(0, 0, 0, 0);
}
.carousel-indicators .active {
    margin: 0;
    width: 12px;
    height: 12px;
    background-color: #fff;
}
.carousel-caption {
    position: absolute;
    left: 15%;
    right: 15%;
    bottom: 20px;
    z-index: 10;
    padding-top: 20px;
    padding-bottom: 20px;
    color: #fff;
    text-align: center;
    text-shadow: 0 1px 2px rgba(0, 0, 0, 0.6);
}
.carousel-caption .btn {
    text-shadow: none;
}
@media screen and (min-width: 768px) {
    .carousel-control .glyphicon-chevron-left,
    .carousel-control .glyphicon-chevron-right,
    .carousel-control .icon-prev,
    .carousel-control .icon-next {
        width: 30px;
        height: 30px;
        margin-top: -15px;
        font-size: 30px;
    }
    .carousel-control .glyphicon-chevron-left,
    .carousel-control .icon-prev {
        margin-left: -15px;
    }
    .carousel-control .glyphicon-chevron-right,
    .carousel-control .icon-next {
        margin-right: -15px;
    }
    .carousel-caption {
        left: 20%;
        right: 20%;
        padding-bottom: 30px;
    }
    .carousel-indicators {
        bottom: 20px;
    }
}
.carousel {
    margin-bottom: 10px;
}
.carousel img {
    margin: 0 auto;
}
.carousel-thumbs {
    list-style: none;
    margin: 0;
    padding: 0;
}
.carousel-thumbs li {
    float: left;
}
.carousel-thumbs li img {
    max-width: 80px;
}
.thumbnail {
    display: block;
    padding: 4px;
    margin-bottom: 20px;
    line-height: 1.42857143;
    background-color: #fff;
    border: 1px solid #ddd;
    border-radius: 4px;
    -webkit-transition: border 0.2s ease-in-out;
    -o-transition: border 0.2s ease-in-out;
    transition: border 0.2s ease-in-out;
}

.thumbnail {
    display: block;
    padding: 4px;
    margin-bottom: 20px;
    line-height: 1.42857143;
    background-color: #fff;
    border: 1px solid #ddd;
    border-radius: 4px;
    -webkit-transition: border 0.2s ease-in-out;
    -o-transition: border 0.2s ease-in-out;
    transition: border 0.2s ease-in-out;
}

.col-xs-3 {
    padding: 0px 15px;
}

</style>
