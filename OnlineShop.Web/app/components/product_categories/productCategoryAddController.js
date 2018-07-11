(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];

    function productCategoryAddController($scope, apiService, notificationService, $state) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        };

        function loadparentCategories() {
            apiService.get('/Api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Cant get list parents');
            });
        }

        loadparentCategories();

        $scope.addProductCategory = addProductCategory;

        function addProductCategory() {
            apiService.post('/api/productcategory/create', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới!');
                $state.go('product_categories');
            }, function () {
                notificationService.displayError('Thêm mới không thành công.');
            });
        }
    }
})(angular.module('onlineshop.product_categories'));