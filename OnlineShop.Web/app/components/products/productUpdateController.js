(function (app) {
    app.controller('productUpdateController', productUpdateController);

    productUpdateController.$inject = ['$scope', 'apiService', '$stateParams', 'notificationService', '$state', 'commonService'];

    function productUpdateController($scope, apiService, $stateParams, notificationService, $state, commonService) {
        $scope.product = {};

        //ckeditor config
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '300px'
        };

        //2 ways binding for alias
        $scope.getSeoTitle = getSeoTitle;
        function getSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        };

        //load product detail
        function getProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function success(result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
            }, function error(error) {
                notificationService.displayError('Không lấy được chi tiết sản phẩm');
                console.log(error);
            });
        };

        //choose image
        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });
            }
            finder.popup();
        }

        //choose more image
        $scope.chooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });
            }
            finder.popup();
        }

        //load product category items
        function getProductCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result
                    .data;
            }, function (error) {
                notificationService.displayError('Không thể lấy danh mục sản phẩm');
            });
        }

        //update product

        $scope.updateProduct = updateProduct;
        function updateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put('/api/product/update/', $scope.product, function success() {
                notificationService.displaySuccess('Cập nhật sản phẩm ' + $scope.product.Name + ' thành công');
                $state.go('products');
            }, function error(error) {
                notificationService.displayError('Không thể cập nhật sản phẩm');
                console.log(error);
            });
        }

        //execution
        getProductDetail();
        getProductCategories();
    };
})(angular.module('onlineshop.products'));