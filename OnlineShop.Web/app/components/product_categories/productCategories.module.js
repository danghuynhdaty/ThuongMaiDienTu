/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('onlineshop.product_categories', ['onlineshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('product_categories', {
            url: '/product_categories',
            templateUrl: '/app/components/product_categories/productCategoryListView.html',
            controller: 'productCategoryListController'
        }).state('add_product_category', {
            url: '/add_product_category',
            templateUrl: '/app/components/product_categories/productCategoryAddView.html',
            controller: 'productCategoryAddController'
        }).state('update_product_category', {
            url: '/update_product_category/:id',
            templateUrl: '/app/components/product_categories/productCategoryUpdateView.html',
            controller: 'productCategoryUpdateController'
        });

        $urlRouterProvider.otherwise('/admin');
    }
})();