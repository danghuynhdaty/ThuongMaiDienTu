(function (app) {
    app.controller('productCategoryUpdateController', productCategoryUpdateController);

    productCategoryUpdateController.$inject = ['$scope', '$state','apiService', 'notificationService', '$stateParams', 'commonService'];

    function productCategoryUpdateController($scope, $state, apiService, notificationService, $stateParams, commonService) {
        $scope.productCategory = {
            UpdateDate: new Date()
        };

        $scope.updateProductCategory = updateProductCategory;
        $scope.getSeoTitle = getSeoTitle;

        function getSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function getProductCategoryDetail() {
            apiService.get('/api/productcategory/getbyid/' + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;
            }, function error(err) {
                notificationService.displayError(err.data);
            });
        }

        function loadParentCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result
                    .data;
            }, function (error) {
                notificationService.displayError('Không thể lấy danh mục cha');
            });
        }

        function updateProductCategory() {
            apiService.put('/api/productcategory/update', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã cập nhật!');
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
            });
        }

        loadParentCategories();
        getProductCategoryDetail();
    }
})(angular.module('onlineshop.product_categories'));