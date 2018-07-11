(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService'];

    function productCategoryListController($scope, apiService, notificationService) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.keyword = '';
        $scope.search = search;

        function search() {
            getProductCategories();
        }

        function getProductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 1
                }
            }
            apiService.get('/api/productcategory/getall', config, function (result) {
                //nếu không tìm thấy một bản ghi nào trong db
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không tìm thấy dữ liệu!');
                }
                //else {
                //    notificationService.displaySuccess('Tìm thấy ' + result.data.TotalCount+' dữ liệu!');
                //}
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load product category failed');
            });
        }

        $scope.getProductCategories();
    }
})(angular.module('onlineshop.product_categories'));