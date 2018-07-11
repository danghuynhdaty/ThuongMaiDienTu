(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];

    function productCategoryAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        };

        $scope.getSeoTitle = getSeoTitle;

        function getSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

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