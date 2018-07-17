(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.products = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getProducts = getProducts;
        $scope.keyword = '';
        $scope.search = search;

        function search() {
            $scope.getProducts();
        }

        function getProducts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            };
            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalCount === 0) {
                    notificationService.displayWarning('Không tìm thấy dữ liệu!');
                }
                else {
                    $scope.products = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                }
            }, function (error) {
                notificationService.displayError('Không tải được dữ liệu');
                console.log(error);
            });
        }

        $scope.getProducts();
    }
})(angular.module('onlineshop.products'));